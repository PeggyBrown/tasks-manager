using FluentAssertions;
using TaskManagerTests;
using Xunit;

namespace xTaskManagerTests
{
    public class CalculatorTests
    {
        private readonly WeakCalculator _calculator = new WeakCalculator();

        [Fact]
        public void ShouldAddValuesCorrectly()
        {
            var result1 = _calculator.Sum(3, 2);
            var result2 = _calculator.Sum(5, 10);
            var result3 = _calculator.Sum(10000, 200);
            Assert.Equal(5, result1);
            Assert.Equal(15, result2);
            Assert.Equal(10200, result3);
        }

        [Fact]
        public void ShouldAddValuesCorrectlyWithFluentAssertions()
        {
            var result = _calculator.Sum(2, 2);
            result.Should().Be(4);
        }


        [Theory]
        [InlineData(2, 3)]
        [InlineData(-5, 10)]
        [InlineData(0, 0)]
        public void Add_ReturnsCorrectSum(int a, int b)
        {
            // Arrange

            // Act
            int sum = _calculator.Sum(a, b);

            // Assert
            Assert.Equal(a + b, sum);
        }
    }
}