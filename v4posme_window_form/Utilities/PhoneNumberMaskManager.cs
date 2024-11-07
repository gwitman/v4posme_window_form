namespace v4posme_window.Utilities
{
    using System;
    using System.ComponentModel;
    using DevExpress.Data.Mask;

    [AutoInstall]
    public class PhoneNumberMaskManager : CustomTextMaskManager
    {
        public static void Register()
        {
            RegisterMaskManagerType(typeof(PhoneNumberMaskManager));
            RegisterMaskManagerInfo(typeof(PhoneNumberMaskManager), null, description: "Mask to enter phone number");
        }
        [Parameters("v4posme_window_form, DevExpress.UITemplates.Collection.Utilities.PhoneNumberMaskManager", "Phone Number")]
        public PhoneNumberMaskManager(
            [DefaultValue(null)][Parameter("tag", "Tag")] object tag = null)
            : base(tag)
        {
        }
        protected override void ProcessCustomTextMaskInput(CustomTextMaskInputArgs ea)
        {
            var number = PhoneNumber.Sanitize(ea.ResultEditText);
            if (ea.CurrentEditText == number && ea.ActionType != CustomTextMaskInputAction.Init)
            {
                ea.Cancel();
                return;
            }
            ea.SetResult(number, Math.Min(number.Length, ea.ResultCursorPosition));
        }
    }
}
