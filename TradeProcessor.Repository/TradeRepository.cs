using System.Threading.Tasks;
using TradeProcessor.Models;

namespace TradeProcessor.Repository
{
    public class TradeRepository : ITradeRepository
    {
        private readonly TradeContext _context;

        public TradeRepository(TradeContext context)
        {
            _context = context;
        }
        public async Task AddTrade(Trade trade)
        {
            var record = new TradeRecord
            {
                Price = (float)trade.Price,
                SourceCurrency = trade.SourceCurrency,
                Lots = trade.Lots,
                DestinationCurrency = trade.DestinationCurrency,
            };

            await _context.AddAsync(record);
            await _context.SaveChangesAsync();
        }
    }

    public interface ITradeRepository
    {
        Task AddTrade(Trade trade);
    }
}
