namespace v4posme_window.Utilities
{

    public static class Luhn
    {
        public static bool IsValid(string number)
        {
            int factor = 1, checkSum = 0;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                int codePoint = number[i] - '0';
                int addend = factor * codePoint;
                factor = factor == 2 ? 1 : 2;
                addend = addend / 10 + addend % 10;
                checkSum += addend;
            }
            return checkSum % 10 == 0;
        }
    }
}
