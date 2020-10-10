using System;
using Microsoft.Extensions.Configuration;
using TradeProcessor.Models;
using TradeProcessor.Services.Contracts;

namespace TradeProcessor.Services.Implementations
{
    public class TradeParserService : ITradeParserService
    {
        private readonly IRegexService _regexService;
        private readonly IConfiguration _configuration;
        private readonly float _lotSize;


        public TradeParserService(IRegexService regexService, IConfiguration configuration) //, float lotSize)
        {
            _regexService = regexService;
            _configuration = configuration;
            
            float.TryParse(_configuration["LotSize"], out _lotSize);
        }

        public Trade Parse(string input)
        {
            var parsedElements = _regexService.Execute(input);

            var tradeAmount = int.Parse(parsedElements[2]);

            if (tradeAmount < 0)
            {
                throw new ArgumentException("Trade Amount must not be a negative number.");
            }

            return new Trade
            {
                SourceCurrency = parsedElements[0],
                DestinationCurrency = parsedElements[1],
                Lots = tradeAmount / _lotSize,
                Price = decimal.Parse(parsedElements[3]),
            };
        }

    }

    public interface ITradeParserService
    {
        Trade Parse(string line);
    }
}