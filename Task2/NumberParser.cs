using System;
using System.Dynamic;
using Task2;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue == null)
                throw new ArgumentNullException("Null string value cannot be parsed.");

            stringValue = stringValue.Trim();
            if (stringValue == "")
                throw new FormatException("Whitespace string cannot be parsed.");

            bool isNegative = stringValue[0] == '-';
            int startIndex = (isNegative || stringValue[0] == '+') ? 1 : 0;
            int result = 0;

            for (int i = startIndex; i < stringValue.Length; i++)
            {
                if (!char.IsDigit(stringValue[i]))
                    throw new FormatException("String contains non-digit characters.");

                int currentDigit = stringValue[i] - '0';

                if ((isNegative && (-result < int.MinValue / 10 || (-result == int.MinValue / 10 && currentDigit > 8)))
                    || (!isNegative && (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && currentDigit > 7))))
                {
                    throw new OverflowException("The number is too large or small to fit in an Int32.");
                }

                result = result * 10 + currentDigit;
            }

            return isNegative ? -result : result;
        }
    }
}
