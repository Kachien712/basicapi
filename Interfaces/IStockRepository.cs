using WebApplication1.DTOs;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetStocksAsync(QueryObject query);
        public Task<Stock?> GetStockAsync(int id);
        public Task<Stock?> GetBySymbolAsync(string symbol);
        public Task<Stock> CreateStockAsync(Stock stockModel);
        public Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDTO stockDTO);
        public Task<Stock?> DeleteStockAsync(int id);
        public Task<bool> StockExist(int id);
    }
}
