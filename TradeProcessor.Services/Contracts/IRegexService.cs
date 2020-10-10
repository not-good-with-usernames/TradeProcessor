using System.Collections.Generic;

namespace TradeProcessor.Services.Contracts
{
    public interface IRegexService
    {
        List<string> Execute(string input);
    }
}