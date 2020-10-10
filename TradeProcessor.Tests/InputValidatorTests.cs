using Microsoft.Extensions.Logging;
using Moq;
using TradeProcessor.Services.Implementations;
using Xunit;

namespace TradeProcessor.Tests
{
    public class InputValidatorTests
    {
        private InputValidator _inputValidator;

        public InputValidatorTests()
        {
            var logger = Mock.Of<ILogger<InputValidator>>();

            _inputValidator = new InputValidator(logger);
        }

        [Theory]
        [InlineData("USDEUR,1,1.5", true)]
        [InlineData("USDEUR,2,1", true)]
        [InlineData("USDEUR,1", false)]
        [InlineData("USDEUR", false)]
        [InlineData("USDEUR,A,1.5", false)]
        [InlineData("USDEUR,A,B", false)]
        [InlineData("USDEUR,1,A", false)]
        [InlineData("USDEUR,1,A.B", false)]
        [InlineData("USDEUR,1.5,1", false)]
        [InlineData("USDE,1.5,1", false)]
        [InlineData("", false)]
        public void InputValidator_GoodInputReturnsTrue_BadReturnsFalse(string input, bool expected)
        {
            var output = _inputValidator.Validate(input);

            Assert.Equal(expected, output);
        }
    }
}