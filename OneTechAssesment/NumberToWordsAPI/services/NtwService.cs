using System;

namespace NumberToWordsAPI.Services
{
    public interface INumberToWordsService
    {
        string ConvertToWords(string number);
    }

    public class NtwService : INumberToWordsService
    {
        public string ConvertToWords(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return "Invalid input";
            }

            if (!decimal.TryParse(number, out var amount))
            {
                return "Invalid number format";
            }

            var wholePart = (long)Math.Floor(amount);
            var fractionalPart = (int)((amount - wholePart) * 100);

            var words = ConvertWholeNumberToWords(wholePart);
            if (fractionalPart > 0)
            {
                words += " AND " + ConvertWholeNumberToWords(fractionalPart) + " CENTS";
            }
            else
            {
                words += " DOLLARS";
            }

            return words;
        }

        private string ConvertWholeNumberToWords(long number)
        {
            if (number == 0) return "ZERO";

            if (number < 0)
            {
                return "MINUS " + ConvertWholeNumberToWords(Math.Abs(number));
            }

            var words = "";

            if ((number / 1000000000) > 0)
            {
                words += ConvertWholeNumberToWords(number / 1000000000) + " BILLION ";
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                words += ConvertWholeNumberToWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertWholeNumberToWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertWholeNumberToWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                var units = new[] { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
                var teens = new[] { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tens = new[] { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 10)
                {
                    words += units[number];
                }
                else if (number < 20)
                {
                    words += teens[number - 10];
                }
                else
                {
                    words += tens[number / 10];
                    if ((number % 10) > 0)
                    {
                        words += "-" + units[number % 10];
                    }
                }
            }

            return words.Trim();
        }
    }
}
