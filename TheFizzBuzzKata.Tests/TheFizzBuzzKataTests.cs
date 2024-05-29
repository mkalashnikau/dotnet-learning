namespace TheFizzBuzzKata.Tests
{
    public class FizzBuzzTests
    {
        [Fact]
        public void FizzBuzz_ReturnsNumber_WhenNotDivisibleByThreeOrFive()
        {
            var result = FizzBuzz.Generate(1, 1);

            Assert.Equal("1", result[0]);
        }

        [Fact]
        public void FizzBuzz_ReturnsFizz_WhenNumberIsDivisibleByThree()
        {
            var result = FizzBuzz.Generate(3, 3);

            Assert.Equal("Fizz", result[0]);
        }
    }
}