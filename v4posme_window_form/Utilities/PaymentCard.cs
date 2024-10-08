namespace DevExpress.UITemplates.Collection.Utilities {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using DevExpress.Utils;

    public sealed class PaymentCard : IEquatable<PaymentCard> {
        public enum Type {
            Undefined,
            [Display(Name = "VISA")]
            Visa,
            [Display(Name = "MasterCard")]
            MasterCard,
            [Display(Name = "Maestro")]
            Maestro,
            [Display(Name = "American Express")]
            AmericanExpress,
            [Display(Name = "Diners Club - International")]
            DinersClubInternational,
            [Display(Name = "Diners Club - Carte Blanche")]
            DinersClubCarteBlanche,
            [Display(Name = "Diners Club - North America")]
            DinersClubNorthAmerica,
            [Display(Name = "JCB")]
            JCB,
            [Display(Name = "Discover")]
            Discover,
            [Display(Name = "Visa Electron")]
            VisaElectron,
            Invalid
        }
        public PaymentCard(string number, DateTime expDate, string cvc) {
            this.Number = number;
            this.NumberMasked = Mask(number ?? string.Empty);
            this.ExpirationDate = expDate;
            this.CVC = cvc;
            this.CardType = PaymentCardType.Guess(Number);
        }
        public PaymentCard(string number, string expDateMonth, string expDateYear, string cvc) {
            this.Number = number;
            this.NumberMasked = Mask(number ?? string.Empty);
            this.ExpirationDate = GetExpirationDate(expDateMonth, expDateYear);
            this.CVC = cvc;
            this.CardType = PaymentCardType.Guess(Number);
        }
        public string Number {
            get;
            private set;
        }
        public string NumberMasked {
            get;
            private set;
        }
        public DateTime ExpirationDate {
            get;
            private set;
        }
        public string CVC {
            get;
            private set;
        }
        public Type CardType {
            get;
            private set;
        }
        public bool Equals(PaymentCard card) {
            if(ReferenceEquals(card, null))
                return false;
            var otherDate = card.ExpirationDate.Date;
            return (Number == card.Number) && (ExpirationDate.Date == otherDate) && (CVC == card.CVC);
        }
        public sealed override bool Equals(object obj) {
            return Equals(obj as PaymentCard);
        }
        public sealed override int GetHashCode() {
            var date = ExpirationDate.Date;
            return HashCodeHelper.CalculateGeneric(Number, date, CVC);
        }
        public override string ToString() {
            if(string.IsNullOrEmpty(Number) && string.IsNullOrEmpty(CVC))
                return "(Empty)";
            return
                PadWithZeros(Number, new string('x', 16)) + Separator +
                ExpirationDate.Date.ToString("MM/yy") + Separator +
                PadWithZeros(CVC, new string('x', 3));
        }
        static string Mask(string number, char maskCharacter = 'x') {
            var builder = new StringBuilder();
            int counter = 0;
            for(int i = 0; i < number.Length; i++) {
                if(char.IsDigit(number[i])) {
                    if(counter < 4 || counter > 11)
                        builder.Append(number[i]);
                    else builder.Append(maskCharacter);
                    if(++counter % 4 == 0)
                        builder.Append(' ');
                }
            }
            return builder.ToString();
        }
        static string PadWithZeros(string number, string @default) {
            return !string.IsNullOrEmpty(number) ? number.PadRight(@default.Length, Zero) : @default;
        }
        // Constants
        public const string Separator = "  ";
        public const char Zero = ' ';
        // Static API
        public static readonly PaymentCard EmptyCard = new PaymentCard(null, DateTime.MinValue, string.Empty);
        public static DateTime GetExpirationDate(string strMonth, string strYear) {
            int month, year;
            if(int.TryParse(strMonth ?? string.Empty, NumberStyles.Integer, CultureInfo.InvariantCulture, out month))
                month = Math.Max(1, Math.Min(month, 12));
            else month = DateTime.Now.Month;
            if(int.TryParse(strYear ?? string.Empty, NumberStyles.Integer, CultureInfo.InvariantCulture, out year))
                year = 2000 + Math.Max(DateTime.Now.Year - 2000, Math.Min(year, DateTime.Now.Year - 2000 + 50));
            else year = DateTime.Now.Year;
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }
    }
}
