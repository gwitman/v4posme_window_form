namespace DevExpress.UITemplates.Collection.Utilities
{
    using System;
    using DevExpress.XtraEditors.Mask;
    using v4posme_window.Utilities;

    public abstract partial class CustomMaskSettings : MaskSettings.User {
        public class PaymentCard : MaskSettingsWithCulture {
            protected override Type GetMaskManagerType() {
                return typeof(PaymentCardMaskManager);
            }
        }
    }
}
