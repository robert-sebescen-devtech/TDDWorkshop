using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TDD;
using Xunit;

namespace TDDStringCalculator.Test
{
	public class StringCalculatorTests
	{
		private StringCalculator _stringCalc;

		public StringCalculatorTests()
		{
			_stringCalc = new StringCalcBuilder().Build();
		}

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

		[Fact]
		public void Add_AnyValidStringInput_ResultShouldBeLogged()
		{
			// ARRANGE
			var validStringInput = "1,2";
			var mockedLogger = new Mock<ILogger>();

			_stringCalc = AStringCalculator().With(mockedLogger).Build();

			// ACT
			_stringCalc.Add(validStringInput);

			// ASSERT
			mockedLogger.Verify(logger => logger.Write(It.IsAny<string>()), Times.Once());
		}

	    [Fact]
	    public void Add_LoggerThrowsAnException_WebServiceShouldBeCalled()
	    {
	        // ARRANGE
	        var inputThatCausesLoggerException = "1,2,3";
	        var stubLogger = new Mock<ILogger>();
	        var mockedWebService = new Mock<IWebService>();
	        var loggerExceptionMessage = "Logging failed!";
	        _stringCalc = AStringCalculator().With(stubLogger).With(mockedWebService).Build();
            stubLogger.Setup(logger => logger.Write(It.IsAny<string>())).Throws(new Exception(loggerExceptionMessage));

            // ACT
	        _stringCalc.Add(inputThatCausesLoggerException);

            // ASSERT
	        mockedWebService.Verify(ws => ws.NotifyLoggingFailed(It.Is<string>((val) => String.Equals(val,loggerExceptionMessage))));
	    }

		private static StringCalcBuilder AStringCalculator()
		{
			return new StringCalcBuilder();
		}
	}

    internal class StringCalcBuilder
	{
		private Mock<ILogger> _logger = new Mock<ILogger>();
        private Mock<IWebService> _webService = new Mock<IWebService>();

        public StringCalcBuilder With(Mock<ILogger> logger)
		{
			_logger = logger;
			return this;
		}

		public StringCalculator Build()
		{
			return new StringCalculator(_logger.Object, _webService.Object);
		}

        public StringCalcBuilder With(Mock<IWebService> mockedWebService)
        {
            _webService = mockedWebService;
            return this;
        }
	}
}
