namespace TheFizzBuzzKata
{
    public class FizzBuzz
    {
        public static List<string> Generate(int start, int end)
        {
            var output = new List<string>();
            for (int i = start; i <= end; i++)
            {
                var s = "";
                if (i % 3 == 0) s += "Fizz";
                if (i % 5 == 0) s += "Buzz";
                if (s == "") s = i.ToString();
                output.Add(s);
            }
            return output;
        }
    }
}
