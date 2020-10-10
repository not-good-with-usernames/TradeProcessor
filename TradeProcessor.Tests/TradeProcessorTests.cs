using System;
using System.Collections.Generic;
using Moq;
using Microsoft.Extensions.Configuration;
using TradeProcessor.Services.Contracts;
using TradeProcessor.Services.Implementations;
using Xunit;

namespace TradeProcessor.Tests
{
    public class TradeParserTests
    {
        private Mock<IConfiguration> _configMock;

        public TradeParserTests()
        {
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(configuration => configuration["RegexPattern"]).Returns(@"(.{1,3})(\\b|.{3}),([\\d]*),(\\d*\\.?\\d*$)");
            _configMock.Setup(configuration => configuration["LotSize"]).Returns(@"10000");
        }

        private static Mock<IRegexService> SetupRegexMock(string tradeAmount)
        {
            var regexMock = new Mock<IRegexService>();
            var value = new List<string>
            {
                "AUD",
                "EUR",
                tradeAmount,
                "1.5"
            };

            regexMock.Setup(regexService => regexService.Execute(It.IsAny<string>())).Returns(value);
            return regexMock;
        }

        [Fact]
        public void TradeParser_LineParses_IntoTradeObject()
        {
            var regexMock = SetupRegexMock("1");

            var tradeParserService = new TradeParserService(regexMock.Object, _configMock.Object);

            var trade = tradeParserService.Parse("I <3 parsing");

            Assert.Equal("AUD", trade.SourceCurrency);
            Assert.Equal("EUR", trade.DestinationCurrency);
            Assert.Equal(1/10000f, trade.Lots);
            Assert.Equal((decimal)1.5, trade.Price);
        }

        [Fact]
        public void TradeParser_NegativeTradAmount_ThrowsException()
        {
            var regexMock = SetupRegexMock("-1");

            var tradeParserService = new TradeParserService(regexMock.Object, _configMock.Object);

            Assert.Throws<ArgumentException>(() => tradeParserService.Parse("AUDEUR,-1,1.5"));
        }
    }
}


