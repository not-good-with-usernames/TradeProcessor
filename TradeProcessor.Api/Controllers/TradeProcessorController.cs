using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeProcessor.Services.Contracts;
using TradeProcessor.Services.Implementations;

namespace TradeProcessor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeProcessorController : ControllerBase
    {
        private readonly ITradeParserService _tradeParserService;
        private readonly ITradeDataService _tradeDataService;
        private readonly ILogger<TradeProcessorController> _logger;

        public TradeProcessorController(ITradeParserService tradeParserService, 
            ITradeDataService tradeDataService,
            ILogger<TradeProcessorController> logger)
        {
            _tradeParserService = tradeParserService;
            _tradeDataService = tradeDataService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]string input)
        {
            try
            {
                var tradeObject = _tradeParserService.Parse(input);

                await _tradeDataService.AddTrade(tradeObject);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);

                throw;
            }

            return Ok();
        }
    }
}
