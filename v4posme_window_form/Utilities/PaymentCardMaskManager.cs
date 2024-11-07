namespace v4posme_window.Utilities
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using DevExpress.Data.Mask;

    [AutoInstall]
    public class PaymentCardMaskManager : CompositeMaskManager<PaymentCard>
    {
        public static void Register()
        {
            RegisterMaskManagerType(typeof(PaymentCardMaskManager));
            RegisterMaskManagerInfo(typeof(PaymentCardMaskManager), null, description: "The mask for credit card numbers.");
        }
        [Parameters("v4posme_window_form, DevExpress.UITemplates.Collection.Utilities.PaymentCardMaskManager", "Card with Number/ExpDate/CVC")]
        public PaymentCardMaskManager(
            [Parameter("cultureInfo")]
            [Parameter("culture", typeof(CultureInfoConverter), "Culture (name)", Visibility = ParameterVisibility.Advanced)]
            CultureInfo cultureInfo)
            : base(cultureInfo)
        {
            cultureInfoForParts = cultureInfo ?? CultureInfo.CurrentCulture;
        }
        CultureInfo cultureInfoForParts;
        protected override MaskManager[] CreateParts(CultureInfo cultureInfo)
        {
            cultureInfoForParts = cultureInfo ?? CultureInfo.CurrentCulture;
            return new MaskManager[] {
                    new SimpleMaskManager(@"0000 0000 0000 0000", cultureInfo, '0', false, true),
                    new RegExpMaskManager(@"(0[1-9]|1[012])", false, true, true, true, '0', cultureInfo, false),
                    new SimpleMaskManager(@"00", cultureInfo, '0', false, true),
                    new SimpleMaskManager(@"000", cultureInfo, '0', false, true),
                };
        }
        static string GetYear(DateTime date)
        {
            return date.ToString("yy", CultureInfo.InvariantCulture);
        }
        static string GetMonth(DateTime date)
        {
            return date.ToString("MM", CultureInfo.InvariantCulture);
        }
        protected override PaymentCard CombinePartsValue()
        {
            string number = Parts[0].GetCurrentEditValue() as string;
            string dateMonth = Parts[1].GetCurrentEditValue() as string;
            string dateYear = Parts[2].GetCurrentEditValue() as string;
            string cvc = Parts[3].GetCurrentEditValue() as string;
            return new PaymentCard(number, PaymentCard.GetExpirationDate(dateMonth, dateYear), cvc);
        }
        protected override int SetPartsInitialEditValue(PaymentCard card)
        {
            Parts[0].SetInitialEditValue(card.Number);
            Parts[0].SelectAll();
            Parts[1].SetInitialEditValue(GetMonth(card.ExpirationDate));
            Parts[1].SelectAll();
            Parts[2].SetInitialEditValue(GetYear(card.ExpirationDate));
            Parts[2].SelectAll();
            Parts[3].SetInitialEditValue(card.CVC);
            Parts[3].SelectAll();
            return 0;
        }
        protected override int SetPartsInitialEmptyValue()
        {
            Parts[0].SetInitialEditValue(null);
            Parts[1].SetInitialEditValue(GetMonth(DateTime.Now));
            Parts[2].SetInitialEditValue(GetYear(DateTime.Now));
            Parts[3].SetInitialEditValue(null);
            return 0;
        }
        protected override string GetPartsSeparator(int index)
        {
            return index != 1 ? PaymentCard.Separator :
                cultureInfoForParts.DateTimeFormat.DateSeparator;
        }
        public override bool Insert(string insertion)
        {
            bool activePartResult = ActivePart.Insert(insertion);
            if (!string.IsNullOrEmpty(insertion) && ActivePart.IsFinal)
                Index = Math.Min(Parts.Length - 1, Index + 1);
            return activePartResult;
        }
        public override bool Backspace()
        {
            bool activePartResult = ActivePart.Backspace();
            if (ActivePart.DisplayCursorPosition == 0 && !activePartResult)
            {
                Index = Math.Max(0, Index - 1);
                return true;
            }
            return activePartResult;
        }
    }
}
