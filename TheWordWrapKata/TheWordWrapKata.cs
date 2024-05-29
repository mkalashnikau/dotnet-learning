using System.Text;

public class WordWrap
{
    public static string Wrap(string input, int length)
    {
        if (input.Length <= length)
        {
            return input;
        }

        var words = input.Split(' ');
        var sb = new StringBuilder();
        var currentLineLength = 0;

        foreach (var word in words)
        {
            if (currentLineLength + word.Length > length && currentLineLength > 0)
            {
                sb.Append('\n');
                currentLineLength = 0;
            }
            else if (currentLineLength > 0)
            {
                sb.Append(' ');
                currentLineLength++;
            }

            sb.Append(word);
            currentLineLength += word.Length;
        }

        return sb.ToString();
    }
}