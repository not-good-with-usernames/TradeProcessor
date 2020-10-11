using System.Collections.Generic;
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

            var inputValidatorMock = new Mock<IInputValidatorService>();
            inputValidatorMock.Setup(validator => validator.Validate(It.IsAny<string>())).Returns(true);

            _regexService = new RegexService(configMock.Object, inputValidatorMock.Object);
        }

        [Fact]
        public void RegexService_ParseValidIntput_IntoFourStrings()
        {
            var output = _regexService.Execute("USDEUR,1,1.5");

            Assert.Equal(4, output.Count);
        }
        
        [Theory]
        [InlineData("USDEUR,1,1.5", 4)]
        [InlineData("USDEUR,2,1", 4)]
        [InlineData("USDEUR,1", 0)]
        [InlineData("USDEUR", 0)]
        [InlineData("USDEUR,A,1.5", 0)]
        [InlineData("USDEUR,A,B", 0)]
        [InlineData("USDEUR,1,A", 0)]
        [InlineData("USDEUR,1,A.B", 0)]
        [InlineData("USDEUR,1.5,1", 0)]
        [InlineData("USDE,1.5,1", 0)]
        [InlineData("", 0)]
        public void RegexService_InputIsCorrectlyParsed(string input, int expected)
        {
            var output = _regexService.Execute(input);

            if (expected == 0)
            {
                Assert.Empty(output);
            }
            else
            {
                Assert.Equal(4, output.Count);
            }
            
        }
    }
}