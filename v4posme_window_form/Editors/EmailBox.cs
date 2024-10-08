namespace DevExpress.UITemplates.Collection.Editors {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using v4posme_window.Assets.Toolbox; 
    using DevExpress.UITemplates.Collection.Images;
    using DevExpress.UITemplates.Collection.Utilities;
    using DevExpress.Utils;
    using DevExpress.Utils.Html;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Internal;
    using DevExpress.XtraEditors.Repository;

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ToolboxBitmapRoot), "EmailBox.bmp")]
    [Description("A predesigned email input field with automatic email address validation and error indication.")]
    public class EmailBox : HtmlTextBoxBase {
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new EmailBoxProperties Properties {
            get { return base.Properties as EmailBoxProperties; }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Email {
            get { return EditValue as string; }
            set { EditValue = value; }
        }
        [Browsable(false)]
        public bool IsEmailInvalid {
            get {
                string email = Text;
                if(string.IsNullOrEmpty(email))
                    return false;
                return !Utilities.Email.Validate(email, out invalidEmailWarningText);
            }
        }
        protected override RepositoryItem CreateRepositoryItemCore() {
            return new EmailBoxProperties();
        }
        #region Properties
        [System.ComponentModel.Category(CategoryName.ToolTip)]
        public bool ShowInvalidEmailWarningToolTip {
            get { return Properties.ShowInvalidEmailWarningToolTip; }
            set { Properties.ShowInvalidEmailWarningToolTip = value; }
        }
        bool ShouldSerializeShowInvalidEmailWarningToolTip() {
            return Properties.ShouldSerializeShowInvalidEmailWarningToolTip();
        }
        void ResetShowInvalidEmailWarningToolTip() {
            Properties.ResetShowInvalidEmailWarningToolTip();
        }
        [DefaultValue(null), System.ComponentModel.Category(CategoryName.ToolTip)]
        public string InvalidEmailWarningTitle {
            get { return Properties.InvalidEmailWarningTitle; }
            set { Properties.InvalidEmailWarningTitle = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [System.ComponentModel.Category("Appearance")]
        public ContextImageOptions LeadingIconOptions {
            get { return Properties.ContextImageOptions; }
        }
        #endregion Properties
        public class EmailBoxProperties : HtmlTextBoxBaseProperties {
            public override BaseEdit CreateEditor() {
                return new EmailBox();
            }
            bool? showInvalidEmailWarningToolTipCore;
            [DefaultValue(true)]
            public bool ShowInvalidEmailWarningToolTip {
                get { return showInvalidEmailWarningToolTipCore.GetValueOrDefault(true); }
                set {
                    if(ShowInvalidEmailWarningToolTip == value) return;
                    showInvalidEmailWarningToolTipCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(ShowInvalidEmailWarningToolTip)));
                }
            }
            protected internal bool ShouldSerializeShowInvalidEmailWarningToolTip() {
                return showInvalidEmailWarningToolTipCore.HasValue;
            }
            protected internal void ResetShowInvalidEmailWarningToolTip() {
                showInvalidEmailWarningToolTipCore = null;
            }
            string invalidEmailWarningTitleCore;
            [DefaultValue(null)]
            public string InvalidEmailWarningTitle {
                get { return invalidEmailWarningTitleCore; }
                set {
                    if(InvalidEmailWarningTitle == value) return;
                    invalidEmailWarningTitleCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(InvalidEmailWarningTitle)));
                }
            }
            protected internal virtual string GetInvalidEmailWarningTitle() {
                return string.IsNullOrEmpty(InvalidEmailWarningTitle) ? "Email address is invalid" : InvalidEmailWarningTitle;
            }
            #region Icons
            protected override ContextImageOptions CreateContextImageOptions() {
                return new EmailContextImageOptions();
            }
            sealed class EmailContextImageOptions : ContextImageOptions {
                public EmailContextImageOptions() {
                    SvgImage = DataEntry.Email;
                    SvgImageSize = new Size(16, 16);
                }
                protected override bool ShouldSerializeSvgImage() {
                    return base.ShouldSerializeSvgImage() && !ReferenceEquals(SvgImage, DataEntry.Email);
                }
                protected override bool ShouldSerializeSvgImageSize() {
                    return base.ShouldSerializeSvgImageSize() && (SvgImageSize != new Size(16, 16));
                }
            }
            #endregion Icons
            #region Parts
            protected internal Rectangle GetLeadingIconBounds() {
                return GetPartBounds(leadingIcon);
            }
            #endregion
            #region Assets
            protected override string LoadDefaultStyles() {
                return EmailBoxHtmlCssAsset.Default.Css;
            }
            protected override string LoadDefaultTemplate() {
                return EmailBoxHtmlCssAsset.Default.Html;
            }
            sealed class EmailBoxHtmlCssAsset : HtmlCssAsset {
                public static readonly HtmlCssAsset Default = new EmailBoxHtmlCssAsset();
            }
            #endregion Style
        }
        protected override void OnEditValueChanging(ChangingEventArgs e) {
            base.OnEditValueChanging(e);
            if(!e.Cancel && !IsEmailInvalid) 
                HideLeadingIconTooltip();
        }
        protected override void OnValidatingCore(CancelEventArgs args) {
            base.OnValidatingCore(args);
            string email = Text;
            if(args.Cancel || string.IsNullOrEmpty(email))
                return;
            if(args.Cancel = !Utilities.Email.Validate(email, out invalidEmailWarningText)) {
                ErrorText = invalidEmailWarningText;
                if(Properties.ShowInvalidEmailWarningToolTip)
                    QueueWarningToolTip(new Action(ShowInvalidEmailWarning));
            }
        }
        string invalidEmailWarningText;
        bool QueueWarningToolTip(Action action = null) {
            if(action == null && Properties.ShowInvalidEmailWarningToolTip && IsEmailInvalid)
                action = new Action(ShowInvalidEmailWarning);
            if(action != null && IsHandleCreated) 
                BeginInvoke(action);
            return (action != null);
        }
        protected virtual void ShowInvalidEmailWarning() {
            var controller = GetToolTipController();
            if(controller == null)
                return;
            string title = Properties.GetInvalidEmailWarningTitle();
            var showArgs = new ToolTipControllerShowEventArgs(this, this, invalidEmailWarningText, title, WarningToolTipLocation, false, false, 0, ToolTipIconType.Warning, ToolTipIconSize.Small, null, -1, controller.Appearance, controller.AppearanceTitle);
            showArgs.ToolTipType = ToolTipType.SuperTip;
            showArgs.ObjectBounds = RectangleToScreen(Properties.GetLeadingIconBounds());
            ShowInvalidEmailWarning(controller, showArgs);
        }
        protected virtual void ShowInvalidEmailWarning(ToolTipController controller, ToolTipControllerShowEventArgs showArgs) {
            ShowLeadingIconTooltip(controller, showArgs);
        }
        protected override bool QueryLeadingIconTooltip() {
            return QueueWarningToolTip();
        }
        protected override ICustomDxHtmlPreview CreateHtmlEditorPreview() {
            var previewControl = new EmailBox();
            previewControl.Properties.BeginUpdate();
            previewControl.Properties.HeaderLabel = string.IsNullOrEmpty(HeaderLabel) ? "{HeaderLabel}" : HeaderLabel;
            previewControl.Properties.FooterLabel = string.IsNullOrEmpty(FooterLabel) ? "{FooterLabel}" : FooterLabel;
            previewControl.Properties.NullValuePrompt = string.IsNullOrEmpty(Placeholder) ? "{Placeholder}" : Placeholder;
            previewControl.Properties.EndUpdate();
            return previewControl;
        }
    }
}
