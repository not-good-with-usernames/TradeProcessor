using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TradeProcessor.Api.Controllers;
using TradeProcessor.Services.Contracts;
using TradeProcessor.Services.Implementations;
using Xunit;

namespace TradeProcessor.Tests
{
    public class TradeProcessorApiTests
    {
        private TradeProcessorController _tradeProcessorController;

        public TradeProcessorApiTests()
        {
            var tps = new Mock<ITradeParserService>();
            var tds = new Mock<ITradeDataService>();
            var logger = new Mock<ILogger<TradeProcessorController>>();

            _tradeProcessorController = new TradeProcessorController(tps.Object, tds.Object, logger.Object);   
        }

        [Fact]
        public async Task TradeProcessorController_GoodInputWorks()
        {
            var result = await _tradeProcessorController.Post("AUDEUR,1,1.5");

            Assert.IsType<OkResult>(result);
        }
    }
}