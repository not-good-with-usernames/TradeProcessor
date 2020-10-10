using Microsoft.Extensions.Configuration;
using Moq;
using TradeProcessor.Services.Contracts;
using TradeProcessor.Services.Implementations;
using Xunit;

namespace TradeProcessor.Tests
{
    public class RegexParserTests
    {
        private RegexService _regexService;

        public RegexParserTests()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(config => config["RegexPattern"]).Returns(@"(.{1,3})(\b|.{3}),([\d]*),(\d*\.?\d*$)");

            var inputValidatorMock = new Mock<IInputValidator>();
            inputValidatorMock.Setup(validator => validator.Validate(It.IsAny<string>())).Returns(true);

            _regexService = new RegexService(configMock.Object, inputValidatorMock.Object);
        }

        [Fact]
        public void RegexService_ParseValidIntput_IntoFourStrings()
        {
            var output = _regexService.Execute("USDEUR,1,1.5");

            Assert.Equal(4, output.Count);
        }
    }
}