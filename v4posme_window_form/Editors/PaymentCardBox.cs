using DevExpress.UITemplates.Collection.Utilities;

namespace v4posme_window.Editors
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using v4posme_window.Assets.Toolbox;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Html;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Internal;
    using DevExpress.XtraEditors.Repository;
    using v4posme_window.Images;
    using v4posme_window.Utilities;

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ToolboxBitmapRoot), "PaymentCardBox.bmp")]
    [Description("A masked field used for credit card/payment information.")]
    public class PaymentCardBox : HtmlTextBoxBase
    {
        protected override RepositoryItem CreateRepositoryItemCore()
        {
            return new PaymentCardBoxProperties();
        }
        #region Properties
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PaymentCardBoxProperties Properties
        {
            get { return BaseProperties as PaymentCardBoxProperties; }
        }
        [Category(CategoryName.ToolTip)]
        public bool ShowInvalidCardWarningToolTip
        {
            get { return Properties.ShowInvalidCardWarningToolTip; }
            set { Properties.ShowInvalidCardWarningToolTip = value; }
        }
        bool ShouldSerializeShowInvalidCardWarningToolTip()
        {
            return Properties.ShouldSerializeShowInvalidCardWarningToolTip();
        }
        void ResetShowInvalidCardWarningToolTip()
        {
            Properties.ResetShowInvalidCardWarningToolTip();
        }
        [DefaultValue(null), Category(CategoryName.ToolTip)]
        public string InvalidCardWarningTitle
        {
            get { return Properties.InvalidCardWarningTitle; }
            set { Properties.InvalidCardWarningTitle = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Appearance")]
        public ContextImageOptions LeadingIconOptions
        {
            get { return Properties.ContextImageOptions; }
        }
        #endregion Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("Data")]
        public PaymentCard Card
        {
            get { return EditValue as PaymentCard; }
            set { EditValue = value; }
        }
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override object EditValue
        {
            get { return base.EditValue; }
            set { base.EditValue = value; }
        }
        public class PaymentCardBoxProperties : HtmlTextBoxBaseProperties
        {
            public override BaseEdit CreateEditor()
            {
                return new PaymentCardBox();
            }
            protected sealed override Type GetDefaultMaskManagerType()
            {
                return typeof(PaymentCardMaskManager);
            }
            protected sealed override bool ShouldCreateMaskSettingsWhenMaskLoadedOrUpdated
            {
                get { return true; }
            }
            protected sealed override bool GetUseMaskAsDisplayFormat()
            {
                return true;
            }
            protected sealed override string GetNullValuePrompt()
            {
                var @base = base.GetNullValuePrompt();
                if (string.IsNullOrEmpty(@base))
                    return $"0000 0000 0000 0000{PaymentCard.Separator}MM/YY{PaymentCard.Separator}000";
                return @base;
            }
            bool? showInvalidCardWarningToolTipCore;
            [DefaultValue(true)]
            public bool ShowInvalidCardWarningToolTip
            {
                get { return showInvalidCardWarningToolTipCore.GetValueOrDefault(true); }
                set
                {
                    if (ShowInvalidCardWarningToolTip == value) return;
                    showInvalidCardWarningToolTipCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(ShowInvalidCardWarningToolTip)));
                }
            }
            protected internal bool ShouldSerializeShowInvalidCardWarningToolTip()
            {
                return showInvalidCardWarningToolTipCore.HasValue;
            }
            protected internal void ResetShowInvalidCardWarningToolTip()
            {
                showInvalidCardWarningToolTipCore = null;
            }
            string invalidCardWarningTitleCore;
            [DefaultValue(null)]
            public string InvalidCardWarningTitle
            {
                get { return invalidCardWarningTitleCore; }
                set
                {
                    if (InvalidCardWarningTitle == value) return;
                    invalidCardWarningTitleCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(InvalidCardWarningTitle)));
                }
            }
            protected internal virtual string GetInvalidCardWarningTitle()
            {
                return string.IsNullOrEmpty(InvalidCardWarningTitle) ? "Card data is invalid" : InvalidCardWarningTitle;
            }
            #region Icons
            protected override ContextImageOptions CreateContextImageOptions()
            {
                return new PaymentCardContextImageOptions(this);
            }
            sealed class PaymentCardContextImageOptions : ContextImageOptions
            {
                readonly PaymentCardBoxProperties owner;
                public PaymentCardContextImageOptions(PaymentCardBoxProperties owner)
                {
                    this.owner = owner;
                    SvgImage = DataEntry.Card;
                    SvgImageSize = new Size(16, 16);
                }
                protected override bool ShouldSerializeSvgImage()
                {
                    return base.ShouldSerializeSvgImage() && !ReferenceEquals(SvgImage, DataEntry.Card);
                }
                protected override bool ShouldSerializeSvgImageSize()
                {
                    return base.ShouldSerializeSvgImageSize() && SvgImageSize != new Size(16, 16);
                }
                [ThreadStatic]
                static Dictionary<int, Image> glyphCache;
                protected override Image GetImageFromSvg(Size userSvgImageSize, bool scaleSvgImageSize, ISvgPaletteProvider palette)
                {
                    palette = CheckSvgPalette(palette);
                    Size size = GetSvgImageSize(SvgImage, SvgImageSize, userSvgImageSize, scaleSvgImageSize, ForcedScaleFactor);
                    var card = ((PaymentCardBox)owner.OwnerEdit)?.Card;
                    if (card != null && card.CardType > PaymentCard.Type.Undefined && card.CardType < PaymentCard.Type.Invalid)
                    {
                        if (glyphCache == null)
                            glyphCache = new Dictionary<int, Image>(CardLogos.SvgImages.Count * 2);
                        int key = HashCodeHelper.Calculate(size.Width, size.Height, (int)card.CardType);
                        Image glyph;
                        if (!glyphCache.TryGetValue(key, out glyph))
                        {
                            glyph = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                            var svgImage = CardLogos.SvgImages[card.CardType.ToString()];
                            using (Graphics g = Graphics.FromImage(glyph))
                            {
                                g.Clear(Color.Transparent);
                                double factor = (double)svgImage.Height / svgImage.Width;
                                float offset = (int)Math.Ceiling((glyph.Height - factor * glyph.Width) / 2);
                                g.TranslateTransform(0, offset);
                                var svgBmp = DevExpress.Utils.Svg.SvgBitmap.Create(svgImage);
                                svgBmp.RenderToGraphics(g, palette, glyph.Width / svgImage.Width);
                            }
                            glyphCache.Add(key, glyph);
                        }
                        return glyph;
                    }
                    return SvgBitmap.Render(size, palette);
                }
            }
            #endregion Icons
            #region Parts
            protected internal Rectangle GetLeadingIconBounds()
            {
                return GetPartBounds(leadingIcon);
            }
            protected override void RaiseEditValueChanged(EventArgs e)
            {
                base.RaiseEditValueChanged(e);
                if (e is ChangingEventArgs args)
                {
                    var prevType = (args.OldValue as PaymentCard)?.CardType;
                    var newType = (args.NewValue as PaymentCard)?.CardType;
                    if (prevType != newType)
                    {
                        InvalidatePart(leadingIcon);
                    }
                }
            }
            #endregion
            #region Assets
            protected override string LoadDefaultStyles()
            {
                return PaymentCardBoxHtmlCssAsset.Default.Css;
            }
            protected override string LoadDefaultTemplate()
            {
                return PaymentCardBoxHtmlCssAsset.Default.Html;
            }
            sealed class PaymentCardBoxHtmlCssAsset : HtmlCssAsset
            {
                public static readonly HtmlCssAsset Default = new PaymentCardBoxHtmlCssAsset();
            }
            #endregion Style
        }
        protected override void OnEditValueChanging(ChangingEventArgs e)
        {
            base.OnEditValueChanging(e);
            if (!e.Cancel && DoValidateCard(out invalidCardWarningText))
                HideLeadingIconTooltip();
        }
        protected override void OnValidatingCore(CancelEventArgs args)
        {
            base.OnValidatingCore(args);
            if (args.Cancel || Card == null)
                return;
            if (args.Cancel = !DoValidateCard(out invalidCardWarningText))
            {
                ErrorText = invalidCardWarningText;
                if (Properties.ShowInvalidCardWarningToolTip)
                    QueueWarningToolTip(new Action(ShowInvalidCardWarning));
            }
        }
        string invalidCardWarningText;
        bool QueueWarningToolTip(Action action = null)
        {
            if (action == null && Properties.ShowInvalidCardWarningToolTip && !DoValidateCard(out invalidCardWarningText))
                action = new Action(ShowInvalidCardWarning);
            if (action != null && IsHandleCreated)
                BeginInvoke(action);
            return action != null;
        }
        protected virtual bool DoValidateCard(out string errorText)
        {
            return DoValidateCard(Card, out errorText);
        }
        protected virtual bool DoValidateCard(PaymentCard card, out string errorText)
        {
            if (card == null)
            {
                errorText = null;
                return true;
            }
            var cardType = PaymentCardType.Validate(card.Number, out errorText);
            bool cardInvalid = cardType == PaymentCard.Type.Invalid;
            if (!cardInvalid && Card.ExpirationDate < DateTime.Now.Date)
            {
                errorText = "Your card is expired";
                cardInvalid = true;
            }
            if (!cardInvalid && (Card.CVC ?? string.Empty).Length != 3)
            {
                errorText = "CVC code is incomplete";
                cardInvalid = true;
            }
            return !cardInvalid && string.IsNullOrEmpty(errorText);
        }
        protected virtual void ShowInvalidCardWarning()
        {
            var controller = GetToolTipController();
            if (controller == null)
                return;
            string title = Properties.GetInvalidCardWarningTitle();
            var showArgs = new ToolTipControllerShowEventArgs(this, this, invalidCardWarningText, title,
                WarningToolTipLocation, false, false, 0, ToolTipIconType.Warning, ToolTipIconSize.Small, null, -1, controller.Appearance, controller.AppearanceTitle);
            showArgs.ToolTipType = ToolTipType.SuperTip;
            showArgs.ObjectBounds = RectangleToScreen(Properties.GetLeadingIconBounds());
            ShowInvalidCardWarning(controller, showArgs);
        }
        protected virtual void ShowInvalidCardWarning(ToolTipController controller, ToolTipControllerShowEventArgs showArgs)
        {
            ShowLeadingIconTooltip(controller, showArgs);
        }
        protected override bool QueryLeadingIconTooltip()
        {
            return QueueWarningToolTip();
        }
        protected override ICustomDxHtmlPreview CreateHtmlEditorPreview()
        {
            var previewControl = new PaymentCardBox();
            previewControl.Properties.BeginUpdate();
            previewControl.Properties.HeaderLabel = string.IsNullOrEmpty(HeaderLabel) ? "{HeaderLabel}" : HeaderLabel;
            previewControl.Properties.FooterLabel = string.IsNullOrEmpty(FooterLabel) ? "{FooterLabel}" : FooterLabel;
            previewControl.Properties.EndUpdate();
            return previewControl;
        }
    }
}
