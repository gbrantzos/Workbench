namespace RunLengthEncoding;

public class RunLengthEncoderTests
{
    [Theory]
    [InlineData("a", "a1")]
    [InlineData("aa", "a2")]
    [InlineData("aaa", "a3")]
    [InlineData("aaabbcccaa", "a3b2c3a2")]
    [InlineData("abbcccccccccc", "a1b2c10")]
    public void Happy_Path(string source, string expected)
    {
        // Arrange - Act
        var rle = new RunLengthEncoder();
        var actual = rle.Encode(source);

        // Assert
        Assert.Equal(expected, actual);
    }
}