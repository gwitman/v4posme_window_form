namespace DevExpress.UITemplates.Collection.Utilities {

    public static class PaymentCardType {
        public static PaymentCard.Type Guess(string cardNumberPrefix) {
            int minLength, maxLength;
            return Guess(cardNumberPrefix, out minLength, out maxLength);
        }
        public static PaymentCard.Type Guess(string cardNumberPrefix, out int minLength, out int maxLength) {
            minLength = maxLength = 16;
            if(string.IsNullOrWhiteSpace(cardNumberPrefix))
                return PaymentCard.Type.Undefined;
            cardNumberPrefix = Normalize(cardNumberPrefix);
            if(cardNumberPrefix.Length > 0) {
                if(cardNumberPrefix[0] == '2' && cardNumberPrefix.Length > 6) {
                    string code = cardNumberPrefix.Substring(1, 5);
                    int prefix;
                    if(int.TryParse(code, out prefix) &&
                        (prefix >= 22100 && prefix <= 72099)) {
                        /* MasterCard: [222100 - 272099] 16 */
                        return PaymentCard.Type.Maestro;
                    }
                }
                if(cardNumberPrefix[0] == '3' && cardNumberPrefix.Length > 1) {
                    if(cardNumberPrefix[1] == '0' && cardNumberPrefix.Length > 2) {
                        if(cardNumberPrefix[2] >= '1' && cardNumberPrefix[2] <= '5') {
                            /* Diners Club - Carte Blanche: [300, 301, 302, 303, 304, 305] 14 */
                            minLength = maxLength = 14;
                            return PaymentCard.Type.DinersClubCarteBlanche;
                        }
                    }
                    if(cardNumberPrefix[1] == '4' || cardNumberPrefix[1] == '7') {
                        /* American Express (AMEX): [34, 37] 15 */
                        minLength = maxLength = 15;
                        return PaymentCard.Type.AmericanExpress;
                    }
                    if(cardNumberPrefix[1] == '5' && cardNumberPrefix.Length > 3) {
                        string code = cardNumberPrefix.Substring(1, 3);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            (prefix >= 528 && prefix <= 589)) {
                            /* JCB: [3528-3589] 16-19 */
                            minLength = 16; maxLength = 19;
                            return PaymentCard.Type.JCB;
                        }
                    }
                    if(cardNumberPrefix[1] == '6') {
                        /* Diners Club - International: [36] 14 */
                        minLength = maxLength = 14;
                        return PaymentCard.Type.DinersClubInternational;
                    }
                }
                if(cardNumberPrefix[0] == '4') {
                    if(cardNumberPrefix.Length > 3) {
                        string code = cardNumberPrefix.Substring(1, 3);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            (prefix == 26 || prefix == 508 || prefix == 844 || prefix == 913 || prefix == 917)) {
                            /* Visa Electron: [4026, 4508, 4844, 4913, 4917] 16 */
                            minLength = maxLength = 16;
                            return PaymentCard.Type.VisaElectron;
                        }
                    }
                    if(cardNumberPrefix.Length > 5) {
                        string code = cardNumberPrefix.Substring(1, 5);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            (prefix == 17500)) {
                            /* Visa Electron: [417500] 16 */
                            minLength = maxLength = 16;
                            return PaymentCard.Type.VisaElectron;
                        }
                    }
                    /* VISA: [4] 13-16-19 */
                    minLength = 16; maxLength = 19;
                    return PaymentCard.Type.Visa;
                }
                if(cardNumberPrefix[0] == '5' && cardNumberPrefix.Length > 1) {
                    if(cardNumberPrefix[1] == '1' || cardNumberPrefix[1] == '2' || cardNumberPrefix[1] == '3' || cardNumberPrefix[1] == '5') {
                        /* MasterCard: [51, 52, 53, 54, 55, 222100 - 272099] 16 */
                        minLength = maxLength = 16;
                        return PaymentCard.Type.MasterCard;
                    }
                    if(cardNumberPrefix[1] == '4') {
                        /* Diners Club -North America: [54] 16 */
                        minLength = maxLength = 16;
                        return PaymentCard.Type.DinersClubNorthAmerica;
                    }
                    if(cardNumberPrefix[1] == '0' || cardNumberPrefix[1] == '8' && cardNumberPrefix.Length > 3) {
                        string code = cardNumberPrefix.Substring(1, 3);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            prefix == 18 || prefix == 20 || prefix == 38 || prefix == 893) {
                            /* Maestro: [5018, 5020, 5038, 5893] 16 - 19 */
                            minLength = 16; maxLength = 19;
                            return PaymentCard.Type.Maestro;
                        }
                    }
                }
                if(cardNumberPrefix[0] == '6' && cardNumberPrefix.Length > 1) {
                    if(cardNumberPrefix[1] == '0' && cardNumberPrefix.Length > 3) {
                        string code = cardNumberPrefix.Substring(1, 3);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            (prefix == 011)) {
                            /* Discover: [6011] 16-19 */
                            minLength = 16; maxLength = 19;
                            return PaymentCard.Type.Discover;
                        }
                    }
                    if(cardNumberPrefix[1] == '2' && cardNumberPrefix.Length > 5) {
                        string code = cardNumberPrefix.Substring(1, 5);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            (prefix >= 22126 && prefix <= 22925)) {
                            /* Discover: [622126-622925] 16-19 */
                            minLength = 16; maxLength = 19;
                            return PaymentCard.Type.Discover;
                        }
                    }
                    if(cardNumberPrefix[1] == '3' && cardNumberPrefix.Length > 2) {
                        string code = cardNumberPrefix.Substring(1, 2);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            (prefix == 37 || prefix == 38 || prefix == 39)) {
                            /* Maestro: [637, 638, 639] 16 */
                            minLength = maxLength = 16;
                            return PaymentCard.Type.Maestro;
                        }
                    }
                    if(cardNumberPrefix[1] == '3' || cardNumberPrefix[1] == '7' && cardNumberPrefix.Length > 3) {
                        string code = cardNumberPrefix.Substring(1, 3);
                        int prefix;
                        if(int.TryParse(code, out prefix)) {
                            if(prefix == 304 || prefix == 759 || prefix == 761 || prefix == 762 || prefix == 762) {
                                /* Maestro: [6304, 6759, 6761, 6762, 6763] 16 - 19 */
                                minLength = 16; maxLength = 19;
                                return PaymentCard.Type.Maestro;
                            }
                        }
                    }
                    if(cardNumberPrefix[1] == '4' && cardNumberPrefix.Length > 2) {
                        string code = cardNumberPrefix.Substring(1, 2);
                        int prefix;
                        if(int.TryParse(code, out prefix) &&
                            (prefix >= 44 && prefix <= 49)) {
                            /* Discover: [644-649] 16-19 */
                            minLength = 16; maxLength = 19;
                            return PaymentCard.Type.Discover;
                        }
                    }
                    if(cardNumberPrefix[1] == '5') {
                        /* Discover: [65] 16-19 */
                        minLength = 16; maxLength = 19;
                        return PaymentCard.Type.Discover;
                    }
                }
            }
            return PaymentCard.Type.Undefined;
        }
        public static PaymentCard.Type Validate(string cardNumber) {
            if(string.IsNullOrWhiteSpace(cardNumber))
                return PaymentCard.Type.Invalid;
            cardNumber = Normalize(cardNumber);
            int min, max;
            var type = Guess(cardNumber, out min, out max);
            if(Luhn.IsValid(cardNumber)) 
                return type;
            return PaymentCard.Type.Invalid;
        }
        public static PaymentCard.Type Validate(string cardNumber, out string error) {
            error = string.Empty;
            if(string.IsNullOrWhiteSpace(cardNumber))
                return PaymentCard.Type.Invalid;
            cardNumber = Normalize(cardNumber);
            int min, max;
            var type = Guess(cardNumber, out min, out max);
            if(cardNumber.Length < min)
                error = "Incomplete card number.";
            if(cardNumber.Length > max)
                error = "The entered card number is too long.";
            if(!Luhn.IsValid(cardNumber)) {
                error = "Invalid card number.";
                return PaymentCard.Type.Invalid;
            }
            return type;
        }
        static string Normalize(string cardNumber) {
            var sb = new System.Text.StringBuilder(cardNumber.Length);
            for(int i = 0; i < cardNumber.Length; i++) {
                if(char.IsDigit(cardNumber[i]))
                    sb.Append(cardNumber[i]);
            }
            return sb.ToString();
        }
    }
}
