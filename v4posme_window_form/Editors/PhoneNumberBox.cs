using DevExpress.UITemplates.Collection.Images;
using DevExpress.UITemplates.Collection.Utilities;
using DevExpress.XtraEditors.Popup;

namespace v4posme_window.Editors
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DevExpress.Data;
    using v4posme_window.Assets.Toolbox;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Drawing;
    using DevExpress.Utils.Html;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Internal;
    using DevExpress.XtraEditors.Repository;
    using v4posme_window.Images;
    using v4posme_window.Utilities;

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ToolboxBitmapRoot), "PhoneNumberBox.bmp")]
    [Description("A masked field used to enter phone numbers, with integrated country selector and auto-formatted user input.")]
    public class PhoneNumberBox : HtmlLookUpBase
    {
        protected override RepositoryItem CreateRepositoryItemCore()
        {
            return new PhoneNumberBoxProperties();
        }
        #region Properties
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PhoneNumberBoxProperties Properties
        {
            get { return BaseProperties as PhoneNumberBoxProperties; }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PhoneNumber
        {
            get { return EditValue as string; }
            set { EditValue = value; }
        }
        [Browsable(false)]
        public bool IsPhoneNumberInvalid
        {
            get
            {
                string phoneNumber = EditValue as string;
                if (string.IsNullOrEmpty(phoneNumber))
                    return false;
                return !Utilities.PhoneNumber.Validate(phoneNumber, out invalidPhoneNumberWarningText, currentCountryInfo);
            }
        }
        [Category(CategoryName.ToolTip)]
        public bool ShowInvalidPhoneNumberWarningToolTip
        {
            get { return Properties.ShowInvalidPhoneNumberWarningToolTip; }
            set { Properties.ShowInvalidPhoneNumberWarningToolTip = value; }
        }
        bool ShouldSerializeShowInvalidPhoneNumberWarningToolTip()
        {
            return Properties.ShouldSerializeShowInvalidPhoneNumberWarningToolTip();
        }
        void ResetShowInvalidPhoneNumberWarningToolTip()
        {
            Properties.ResetShowInvalidPhoneNumberWarningToolTip();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Appearance")]
        public ContextImageOptions LeadingIconOptions
        {
            get { return Properties.LeadingIconOptions; }
        }
        #endregion Properties
        public class PhoneNumberBoxProperties : HtmlLookUpBaseProperties, DevExpress.Utils.Text.IStringImageProvider
        {
            public PhoneNumberBoxProperties()
            {
                AutoSuggest += OnAutoSuggest;
                CustomDrawCell += OnCustomDrawCell;
                GetNotInListValue += OnGetNotInListValue;
            }
            protected override void Dispose(bool disposing)
            {
                AutoSuggest -= OnAutoSuggest;
                CustomDrawCell -= OnCustomDrawCell;
                GetNotInListValue -= OnGetNotInListValue;
                base.Dispose(disposing);
            }
            public override void PopulateColumns()
            {
                BeginUpdate();
                Columns.Clear();
                Columns.Add(new LookUpColumnInfo("Phone"));
                EndUpdate();
            }
            public override BaseEdit CreateEditor()
            {
                return new PhoneNumberBox();
            }
            protected sealed override Type GetDefaultMaskManagerType()
            {
                return typeof(PhoneNumberMaskManager);
            }
            protected sealed override bool ShouldCreateMaskSettingsWhenMaskLoadedOrUpdated
            {
                get { return true; }
            }
            protected sealed override bool GetUseMaskAsDisplayFormat()
            {
                return true;
            }
            bool? showFooterCore;
            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override bool ShowFooter
            {
                get { return showFooterCore.GetValueOrDefault(); }
                set { showFooterCore = value; }
            }
            bool? showHeaderCore;
            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public override bool ShowHeader
            {
                get { return showHeaderCore.GetValueOrDefault(); }
                set { showHeaderCore = value; }
            }
            void OnGetNotInListValue(object sender, GetNotInListValueEventArgs e)
            {
                if (e.FieldName == "Phone")
                    e.Value = GetPhone(DataAdapter.GetRowByListSourceIndex(e.RecordIndex) as Countries.Info);
            }
            void OnAutoSuggest(object sender, LookUpEditAutoSuggestEventArgs e)
            {
                var currentCountry = ((PhoneNumberBox)sender).currentCountryInfo;
                string digits = Utilities.PhoneNumber.GetDigits(e.Text ?? string.Empty);
                if (string.IsNullOrEmpty(digits) && !string.IsNullOrEmpty(e.Text))
                    e.Cancel = true;
                else
                {
                    e.QuerySuggestions = Task.Run(GetGetCountries(digits, currentCountry, e.CancellationToken));
                    e.SetHighlightRange(GetGetRangeFromDisplayText(digits, currentCountry));
                }
            }
            void OnCustomDrawCell(object sender, LookUpCustomDrawCellArgs e)
            {
                var info = e.Row as Countries.Info;
                if (info != null)
                {
                    e.DrawHtmlText(GetHtmlRepresentation(info));
                    e.Handled = true;
                }
            }
            internal static Func<ICollection> GetGetCountries(string digits,
                Countries.Info currentCountry,
                CancellationToken cancellationToken)
            {
                Func<string, bool> startsWith = code =>
                    digits.StartsWith(code, StringComparison.OrdinalIgnoreCase) ||
                    code.StartsWith(digits, StringComparison.OrdinalIgnoreCase);
                if (string.IsNullOrEmpty(digits) || IsCurrentCountry(digits, currentCountry))
                    startsWith = _ => true;
                var entries = Countries.All;
                return new Func<ICollection>(() => entries.Where(x =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return startsWith(x.PhoneCodeDigits);
                }).ToList());
            }
            internal static Func<string, DisplayTextHighlightRange?> GetGetRangeFromDisplayText(string digits,
                Countries.Info currentCountry)
            {
                if (string.IsNullOrEmpty(digits) || IsCurrentCountry(digits, currentCountry))
                    return _ => null;
                return (code) =>
                {
                    int codeStart = code.LastIndexOf('+');
                    int codeLength = Math.Min(digits.Length, code.Length - codeStart - 1);
                    int start = -1, end = -1, currentCodePos = codeStart + 1, currentDigitsPos = 0;
                    for (; currentCodePos < code.Length; currentCodePos++)
                    {
                        char c = code[currentCodePos];
                        if (char.IsDigit(c))
                        {
                            if (currentDigitsPos >= digits.Length || digits[currentDigitsPos++] != c)
                                break;
                            if (start < 0)
                                start = currentCodePos;
                            end = currentCodePos + 1;
                        }
                    }
                    if (start < 0)
                        return null;
                    return new DisplayTextHighlightRange(start, end - start);
                };
            }
            static bool IsCurrentCountry(string digits, Countries.Info currentCountry)
            {
                return currentCountry != null && string.Equals(digits, currentCountry.PhoneCodeDigits, StringComparison.OrdinalIgnoreCase);
            }
            static string GetPhone(Countries.Info info)
            {
                return $"{info.ISO2}{info.Country}+{info.PhoneCode}";
            }
            internal static string GetHtmlRepresentation(Countries.Info info)
            {
                return $"<image={info.ISO2}><nbsp>{info.Country}<nbsp><color=@disabledtext>+{info.PhoneCode}</color>";
            }
            protected override SearchMode GetDefaultSearchMode()
            {
                return SearchMode.AutoSuggest;
            }
            bool? showInvalidPhoneNumberWarningToolTipCore;
            public bool ShowInvalidPhoneNumberWarningToolTip
            {
                get { return showInvalidPhoneNumberWarningToolTipCore.GetValueOrDefault(true); }
                set
                {
                    if (ShowInvalidPhoneNumberWarningToolTip == value) return;
                    showInvalidPhoneNumberWarningToolTipCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(ShowInvalidPhoneNumberWarningToolTip)));
                }
            }
            protected internal bool ShouldSerializeShowInvalidPhoneNumberWarningToolTip()
            {
                return showInvalidPhoneNumberWarningToolTipCore.HasValue;
            }
            protected internal void ResetShowInvalidPhoneNumberWarningToolTip()
            {
                showInvalidPhoneNumberWarningToolTipCore = null;
            }
            #region Icons
            Image DevExpress.Utils.Text.IStringImageProvider.GetImage(string id)
            {
                var scaleDpi = ((PhoneNumberBox)OwnerEdit)?.ScaleDPI ?? DevExpress.Utils.DPI.ScaleHelper.NoScale;
                var imgSize = scaleDpi.ScaleSize(ContextImageOptions.SvgImageSize);
                return CountryFlags.GetISO2GlyphOrFlag(LookAndFeel, id, imgSize);
            }
            protected override ContextImageOptions CreateContextImageOptions()
            {
                return new PhoneNumberBoxContextImageOptions(this);
            }
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ContextImageOptions LeadingIconOptions
            {
                get { return ContextImageOptions; }
            }
            sealed class PhoneNumberBoxContextImageOptions : ContextImageOptions
            {
                readonly PhoneNumberBoxProperties owner;
                public PhoneNumberBoxContextImageOptions(PhoneNumberBoxProperties owner)
                {
                    this.owner = owner;
                    SvgImage = DataEntry.Phone;
                    SvgImageSize = new Size(16, 16);
                }
                protected override bool ShouldSerializeSvgImage()
                {
                    return base.ShouldSerializeSvgImage() && !ReferenceEquals(SvgImage, DataEntry.Phone);
                }
                protected override bool ShouldSerializeSvgImageSize()
                {
                    return base.ShouldSerializeSvgImageSize() && SvgImageSize != new Size(16, 16);
                }
                Countries.Info ChoosenCountry
                {
                    get { return ((PhoneNumberBox)owner.OwnerEdit)?.currentCountryInfo; }
                }
                protected override Image GetImageFromSvg(Size userSvgImageSize, bool scaleSvgImageSize, ISvgPaletteProvider palette)
                {
                    palette = CheckSvgPalette(palette);
                    Size size = GetSvgImageSize(SvgImage, SvgImageSize, userSvgImageSize, scaleSvgImageSize, ForcedScaleFactor);
                    var choosenCountry = ChoosenCountry;
                    if (choosenCountry != null)
                        return CountryFlags.GetISO2GlyphOrFlag(owner.LookAndFeel, choosenCountry.ISO2, size, palette);
                    return SvgBitmap.Render(size, palette);
                }
            }
            #endregion Icons
            #region Parts
            protected static readonly object separator = new object();
            //
            protected override void FindParts(Dictionary<object, DxHtmlElement> parts, DxHtmlRootElement root)
            {
                base.FindParts(parts, root);
                parts[separator] = root.FindElementById(nameof(separator));
            }
            protected override void CalcPartsVisibility(Dictionary<object, DxHtmlElement> parts, object editValue)
            {
                base.CalcPartsVisibility(parts, editValue);
                parts[separator].Hidden = !IsDropDownIconVisible();
            }
            protected override object GetPartImage(string partName, ObjectState state)
            {
                if (partName == "DropDownIcon")
                    return UIElements.DropDown;
                return base.GetPartImage(partName, state);
            }
            protected internal Rectangle GetLeadingIconBounds()
            {
                return GetPartBounds(leadingIcon);
            }
            protected override bool IsDropDownIconVisible()
            {
                return Countries.All.Count > 0;
            }
            #endregion
            #region Assets
            protected override string LoadDefaultStyles()
            {
                return PhoneNumberBoxHtmlCssAsset.Default.Css;
            }
            protected override string LoadDefaultTemplate()
            {
                return PhoneNumberBoxHtmlCssAsset.Default.Html;
            }
            sealed class PhoneNumberBoxHtmlCssAsset : HtmlCssAsset
            {
                public static readonly HtmlCssAsset Default = new PhoneNumberBoxHtmlCssAsset();
            }
            #endregion Style
        }
        protected override bool QueryLeadingIconTooltip()
        {
            return QueueWarningToolTip();
        }
        protected override void OnValidatingCore(CancelEventArgs args)
        {
            base.OnValidatingCore(args);
            string phoneNumber = EditValue as string;
            if (args.Cancel || string.IsNullOrEmpty(phoneNumber))
                return;
            if (args.Cancel = !Utilities.PhoneNumber.Validate(phoneNumber, out invalidPhoneNumberWarningText, currentCountryInfo))
            {
                ErrorText = invalidPhoneNumberWarningText;
                if (Properties.ShowInvalidPhoneNumberWarningToolTip)
                    QueueWarningToolTip(new Action(ShowInvalidPhoneNumberWarning));
            }
        }
        string invalidPhoneNumberWarningText;
        bool QueueWarningToolTip(Action action = null)
        {
            if (action == null && Properties.ShowInvalidPhoneNumberWarningToolTip && IsPhoneNumberInvalid)
                action = new Action(ShowInvalidPhoneNumberWarning);
            if (action != null && IsHandleCreated)
                BeginInvoke(action);
            return action != null;
        }
        protected virtual void ShowInvalidPhoneNumberWarning()
        {
            var controller = GetToolTipController();
            if (controller == null)
                return;
            string title = "Phone number is invalid";
            var showArgs = new ToolTipControllerShowEventArgs(this, this, invalidPhoneNumberWarningText, title, WarningToolTipLocation, false, false, 0, ToolTipIconType.Warning, ToolTipIconSize.Small, null, -1, controller.Appearance, controller.AppearanceTitle);
            showArgs.ToolTipType = ToolTipType.SuperTip;
            showArgs.ObjectBounds = RectangleToScreen(Properties.GetLeadingIconBounds());
            ShowInvalidPhoneNumberWarning(controller, showArgs);
        }
        protected virtual void ShowInvalidPhoneNumberWarning(ToolTipController controller, ToolTipControllerShowEventArgs showArgs)
        {
            ShowLeadingIconTooltip(controller, showArgs);
        }
        Countries.Info currentCountryInfo;
        protected override void OnEditValueChanging(ChangingEventArgs e)
        {
            if (!e.Cancel)
                e.NewValue = Utilities.PhoneNumber.EnsureCountry(e.NewValue, e.OldValue, ref currentCountryInfo);
            base.OnEditValueChanging(e);
            if (!e.Cancel && !IsPhoneNumberInvalid)
                HideLeadingIconTooltip();
        }
        protected override ICustomDxHtmlPreview CreateHtmlEditorPreview()
        {
            var previewControl = new PhoneNumberBox();
            previewControl.Properties.BeginUpdate();
            previewControl.Properties.HeaderLabel = string.IsNullOrEmpty(HeaderLabel) ? "{HeaderLabel}" : HeaderLabel;
            previewControl.Properties.FooterLabel = string.IsNullOrEmpty(FooterLabel) ? "{FooterLabel}" : FooterLabel;
            previewControl.Properties.Placeholder = string.IsNullOrEmpty(Placeholder) ? "{Placeholder}" : Placeholder;
            previewControl.Properties.EndUpdate();
            return previewControl;
        }
        #region AutoSuggest vs Mask
        // below we override some methods in order to avoid conflicts with masked input and autosuggest-autocompletion
        protected override void UpdateSearchTextOnAutoSuggest(LookUpEditSearchHighlightEventArgs args, bool autoSuggestValueFound)
        {
            /* do nothing */
        }
        protected override void UpdateSearchTextOnAutoSuggest(LookUpEditSearchHighlightEventArgs args, KeyPressHelper helper, bool? keepSelection)
        {
            /* do nothing */
        }
        protected override bool IsAutoSearchCharHandled(KeyPressHelper helper, KeyPressEventArgs eventArgs)
        {
            return false;
        }
        protected override bool FindAutoSuggestValue(KeyPressHelper helper)
        {
            return false;
        }
        protected override bool AcceptEditorTextAsPopupAutoSearchValue()
        {
            return true;
        }
        protected override void OnControllerValueChanged(Func<object> controllerGetEditValue)
        {
            BaseControllerValueChanged(controllerGetEditValue);
        }
        protected override void OnControllerValueChanging(ChangingEventArgs e)
        {
            BaseControllerValueChanging(e);
        }
        #endregion
    }
}
