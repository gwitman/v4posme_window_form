namespace v4posme_window.Utilities
{
    using System;
    using System.Text;

    public static class PhoneNumber
    {
        const int PhoneNumberLength = 11;
        const int PhoneNumberMaxLength = 15;
        const int CountryCodeMaxLength = 5;
        //
        public static bool Validate(string phoneNumber, out string error, Countries.Info info = null)
        {
            error = string.Empty;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                error = "Empty phone number.";
                return false;
            }
            var digits = new StringBuilder(PhoneNumberLength);
            int index = 0, openBracketIndex = -1, plusIndex = -1;
            foreach (char c in phoneNumber)
            {
                if (char.IsDigit(c))
                {
                    digits.Append(c);
                    index++;
                }
                else
                {
                    if (IsValidCharacter(ref index, ref openBracketIndex, ref plusIndex, c))
                        continue;
                    error = "Phone number includes invalid character(s): " + c.ToString();
                    return false;
                }
            }
            if (openBracketIndex >= 0 && digits.Length > CountryCodeMaxLength)
            {
                error = "The country code section has no closing bracket: (";
                return false;
            }
            string phoneNumberDigits = digits.ToString();
            Countries.Info validationInfo;
            if (!Countries.IsValidPhoneCode(phoneNumberDigits, out validationInfo))
            {
                error = "Invalid country code.";
                return false;
            }
            if (info == null || !Countries.HasTheSamePhoneCode(validationInfo, phoneNumberDigits))
                info = validationInfo;
            if (phoneNumberDigits.Length >= PhoneNumberMaxLength)
                error = "The number is too long.";
            if (phoneNumberDigits.Length < PhoneNumberLength)
            {
                int digitsRemains = PhoneNumberLength - phoneNumberDigits.Length;
                error =
                    $"The number is too short." + Environment.NewLine +
                    $"A valid phone number for {info.Country} requires {digitsRemains} more digit(s).";
            }
            return string.IsNullOrEmpty(error);
        }
        public static string GetDigits(string phoneNumber, bool throwOnErrors = false)
        {
            var digits = new StringBuilder(PhoneNumberLength);
            int index = 0, openBracketIndex = -1, plusIndex = -1;
            foreach (char c in phoneNumber)
            {
                if (char.IsDigit(c))
                {
                    digits.Append(c);
                    index++;
                }
                else
                {
                    if (IsValidCharacter(ref index, ref openBracketIndex, ref plusIndex, c))
                        continue;
                    if (throwOnErrors)
                        throw new NotSupportedException("Phone number includes invalid character: " + c.ToString());
                }
            }
            if (openBracketIndex >= 0 && digits.Length > CountryCodeMaxLength)
            {
                if (throwOnErrors)
                    throw new NotSupportedException("The country code section has no closing bracket: (");
            }
            return digits.ToString();
        }
        public static string Sanitize(string phoneNumber)
        {
            var digits = new StringBuilder(PhoneNumberLength);
            int index = 0, openBracketIndex = -1, plusIndex = -1;
            int tmp = -1, firstBracket = -1;
            char prevChar = char.MinValue;
            int validDigitsCount = 0;
            foreach (char c in phoneNumber)
            {
                if (char.IsDigit(c))
                {
                    if (++validDigitsCount > PhoneNumberMaxLength)
                        break;
                    digits.Append(c);
                    index++;
                    prevChar = c;
                }
                else
                {
                    tmp = openBracketIndex;
                    if (IsValidCharacter(ref index, ref openBracketIndex, ref plusIndex, c))
                    {
                        if (openBracketIndex != tmp && tmp == -1)
                            firstBracket = digits.Length;
                        if (char.IsWhiteSpace(c))
                        {
                            if (!char.IsWhiteSpace(prevChar))
                                digits.Append(' ');
                        }
                        else if (c == '-')
                        {
                            if (char.IsDigit(prevChar))
                                digits.Append(c);
                        }
                        else digits.Append(c);
                        prevChar = c;
                    }
                }
            }
            if (openBracketIndex >= 0 && digits.Length > 9 && firstBracket >= 0)
                digits.Remove(firstBracket, 1);
            return digits.ToString();
        }
        public static string EnsureCountry(object numberOrCountry, object oldNumberOrCountry, ref Countries.Info currentCountry)
        {
            string number = numberOrCountry as string;
            if (!string.IsNullOrEmpty(number))
            {
                string digits = GetDigits(number);
                Countries.Info info;
                if (Countries.IsValidPhoneCode(digits, out info))
                {
                    if (currentCountry == null || currentCountry.PhoneCodeDigits != info.PhoneCodeDigits)
                        currentCountry = info;
                }
            }
            var country = numberOrCountry as Countries.Info;
            if (country != null)
            {
                if (currentCountry == country)
                    return oldNumberOrCountry as string ?? "+" + country.PhoneCode;
                var prevCountry = currentCountry;
                currentCountry = country;
                if (prevCountry != null && prevCountry.PhoneCodeDigits == country.PhoneCodeDigits)
                    return oldNumberOrCountry as string ?? "+" + country.PhoneCode;
                else
                    number = "+" + currentCountry.PhoneCode;
            }
            return number;
        }
        //
        static bool IsValidCharacter(ref int index, ref int openBracketIndex, ref int plusIndex, char c)
        {
            if (char.IsWhiteSpace(c) || c == '-' && index > 0)
            {
                index++;
                return true;
            }
            if (c == '(' && openBracketIndex == -1)
            {
                if (plusIndex == 0 && index > 1)
                {
                    openBracketIndex = index++;
                    return true;
                }
                if (plusIndex == -1 && index == 0)
                {
                    openBracketIndex = index++;
                    return true;
                }
            }
            if (c == ')' && openBracketIndex >= 0 && index > openBracketIndex + 1)
            {
                openBracketIndex = -1; index++;
                return true;
            }
            if (c == '+' && plusIndex == -1)
            {
                if (index == 0 || index == 1 && openBracketIndex == 0)
                {
                    plusIndex = index++;
                    return true;
                }
            }
            return false;
        }
    }
}
