using System.Threading.Tasks;
using TradeProcessor.Models;

namespace TradeProcessor.Services.Contracts
{
    public interface ITradeDataService
    {
        Task AddTrade(Trade trade);
    }
}