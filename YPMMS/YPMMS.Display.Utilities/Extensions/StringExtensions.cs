namespace YPMMS.Display.Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert an ISO 4217 currency code into its currency symbol (if known)
        /// </summary>
        /// <param name="currencyCode">Currency code to convert</param>
        /// <returns>Symbol suitable for prepending to a figure, or else the currency code with a space appended</returns>
        public static string ToCurrencySymbol(this string currencyCode)
        {
            // Bit rough and ready: this should be replaced eventually with a proper lookup table
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                return string.Empty;
            }

            switch (currencyCode)
            {
                case "GBP":
                    return "£";
                case "EUR":
                    return "€";
                case "USD":
                case "AUD":
                case "HKD":
                    return "$";
                case "JPY":
                    return "¥";
                default:
                    return currencyCode + " ";
            }
        }
    }
}