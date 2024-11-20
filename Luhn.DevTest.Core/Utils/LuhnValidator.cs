namespace Luhn.DevTest.Core.Utils
{
    public static class LuhnValidator
    {
        public static bool ValidateCreditCard(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber)) return false;

            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(cardNumber[i])) return false;

                int n = int.Parse(cardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }

                sum += n;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }
    }
}
