using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.CompilerServices;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Stock;
using WebApplication1.Models;

namespace WebApplication1.Mappers
{
    public static class StockMappers
    {
        public static StockDTO ToStockDTO (this Stock stockModel)
        {
            return (new StockDTO
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            });
        }

        public static Stock ToStockCreateRequestDTO(this CreateStockRequestDTO stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
            };
        }
    }
}
