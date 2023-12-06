using NUnit.Framework;

namespace TaskManagerTests
{
    [TestFixture]
    public class CalculatorTest
    {
        private readonly WeakCalculator _calculator = new WeakCalculator();
       
        [Test]
        public void ShouldAddValuesCorrectly()
        {
            var result = _calculator.Sum(2, 2);
            Assert.AreEqual(4, result);
        }
        
        [Test]
        public void ShouldAddOtherValuesCorrectly()
        {
            var result = _calculator.Sum(2, 3);
            Assert.AreEqual(5, result);
        }
        
        [Test]
        public void ShouldMultiplyValuesCorrectly()
        {
            var result = _calculator.Multiply(2, 3);
            Assert.AreEqual(6, result);
        }
    }
}