using System.Text;

namespace RunLengthEncoding;

public class RunLengthEncoder
{
    /*

    Create two methods, one for run-length encoding and one for run-length decoding

    For the Run Length Encoding:

    The input will be a string, the return should be an RLE string.
    Example:
    aaabbcccaa -> a3b2c3a2

    For the Run Length Decoding

    The input will be a valid RLE string, the return should be the string.
    Example:
    a3b2c3a2 -> aaabbcccaa
    a1b2c10 -> abbcccccccccc

    */
    public string Encode(string source)
    {
        if (String.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));

        var sb = new StringBuilder();
        var count = 1;
        char current = source[0];

        for (var i = 1; i < source.Length; i++)
        {
            if (current == source[i])
                count++;
            else
            {
                sb.Append($"{current}{count}");
                current = source[i];
                count = 1;
            }
        }

        sb.Append($"{current}{count}");

        return sb.ToString();
    }
}