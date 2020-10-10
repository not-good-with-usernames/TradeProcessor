using System.Threading.Tasks;
using TradeProcessor.Models;
using TradeProcessor.Repository;
using TradeProcessor.Services.Contracts;

namespace TradeProcessor.Services.Implementations
{
    public class TradeDataService : ITradeDataService
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeDataService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }
        public async Task AddTrade(Trade trade)
        {
            await _tradeRepository.AddTrade(trade);
        }
    }
}
