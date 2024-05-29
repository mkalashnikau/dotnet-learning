using Xunit;

public class WordWrapTests
{
    [Theory]
    [InlineData("test", 7, "test")]
    [InlineData("hello world", 7, "hello\nworld")]
    [InlineData("hello world", 5, "hello\nworld")]
    [InlineData("A lot of words", 5, "A lot\nof\nwords")]
    [InlineData("this is a test", 4, "this\nis a\ntest")]
    public void Wrap_WhenCalled_ShouldWrapTextAtSpecifiedLength(string input, int length, string expectedResult)
    {
        string result = WordWrap.Wrap(input, length);

        Assert.Equal(expectedResult, result);
    }
}