namespace StringSumKata
{
    public class StringCalculator
    {
        public static string Sum(string num1, string num2)
        {
            int intNum1;
            int intNum2;

            if (!int.TryParse(num1, out intNum1) || !int.TryParse(num2, out intNum2))
            {
                return "0";
            }

            int sum = intNum1 + intNum2;

            return sum.ToString();
        }
    }
}
