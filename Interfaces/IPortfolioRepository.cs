using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IPortfolioRepository
    {
        public Task<List<Stock>> GetUserPortfolio(AppUser user);
        public Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);
        public Task<Portfolio> DeletePortfolioAsync(AppUser appUser, String symbol);
    }
}
