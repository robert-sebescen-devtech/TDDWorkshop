using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD;
using Xunit;

namespace TDDStringCalculator.Test
{
    public class StringCalculatorTests
    {
        StringCalculator _stringCalc = new StringCalculator();
        [Fact]
        public void Add_ForEmptyStringInput_ReturnsZero()
        {
            //ARRANGE
            int expected = 0;
            string emptyString = "";
            
            //ACT
            int actual = _stringCalc.Add(emptyString);

            //ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ForOneNumberAsStringInput_ReturnThatNumber()
        {
            // ARRANGE
            int expected = 5;
            string oneNumberAsString = "5";

            // ACT 
            int actual = _stringCalc.Add(oneNumberAsString);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ForCommaSeparatedTwoNumbersAsStringInput_ReturnsSumOfThoseNumbers()
        {
            // ARRANGE
            int expected = 10;
            string twoNumbersAsString = "3,7";
            
            // ACT
            int actual = _stringCalc.Add(twoNumbersAsString);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ForAnyAmountOfNumbersAsStringInput_ReturnsTheirSum()
        {
            // ARRANGE
            int expected = 6;
            string threeNumbers = "1,2,3";

            // ACT
            int actual = _stringCalc.Add(threeNumbers);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ForCommaAndNewLineSeparatedNumbersAsStringInput_ReturnTheirSum()
        {
            // ARRANGE
            int expected = 6;
            string newLineInsteadOfCommaString = "1\n2,3";

            // ACT
            int actual = _stringCalc.Add(newLineInsteadOfCommaString);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ProvidingDelimiterWithNumersAsStringInput_ReturnTheirSum()
        {
            // ARRANGE
            int expected = 3;
            string numbersWithDelimiter = "//;\n1;2";

            // ACT
            int actual = _stringCalc.Add(numbersWithDelimiter);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_ProvidingNegativeNumbersAsStringInput_ExceptionShouldBeThrown()
        {
            // ARRANGE
            string inputWithNegativeNumbers = "1,2,-3";
            string expectedExceptionMessage = "-3";

            // ACT
            var ex = Assert.Throws<Exception>(() => _stringCalc.Add(inputWithNegativeNumbers));
            Assert.Equal(expectedExceptionMessage, ex.Message);
        }
        
    }
}
