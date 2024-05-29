namespace StringSumKata
{
    public class StringSumTests
    {
        [Fact]
        public void Sum_ReturnsZero_WhenInputStringsAreEmpty()
        {
            var result = StringCalculator.Sum("", "");

            Assert.Equal("0", result);
        }

        [Theory]
        [InlineData("1", "2", "3")]
        [InlineData("3", "7", "10")]
        public void Sum_ReturnsSumOfInputStrings_WhenBothInputsAreNumbers(string num1, string num2, string expected)
        {
            var result = StringCalculator.Sum(num1, num2);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("a", "2", "0")]
        [InlineData("3", "b", "0")]
        public void Sum_ReturnsZero_WhenAtLeastOneInputIsNotANumber(string num1, string num2, string expected)
        {
            var result = StringCalculator.Sum(num1, num2);

            Assert.Equal(expected, result);
        }
    }
}