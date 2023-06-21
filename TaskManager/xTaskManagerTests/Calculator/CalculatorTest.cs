using System;
using FluentAssertions;
using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;
using TaskManagerTests;
using Xunit;
using Xunit.Abstractions;

namespace xTaskManagerTests
{
    public class CalculatorTests
    {
        private readonly WeakCalculator _calculator = new WeakCalculator();
        private readonly ITestOutputHelper _output;

        public CalculatorTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldAddValuesCorrectly()
        {
            var result = _calculator.Sum(2, 2);
            Assert.Equal(4, result);
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