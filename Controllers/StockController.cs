using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Stock;
using WebApplication1.Helpers;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDbContext dbContext, IStockRepository stockRepo) 
        {
            _dbContext = dbContext;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetStocks([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stocks = await _stockRepo.GetStocksAsync(query);
            var stockDTO = stocks.Select(x => x.ToStockDTO()).ToList();
            return Ok(stockDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStock([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = (await _stockRepo.GetStockAsync(id))
                ?.ToStockDTO();
            if (stock != null)
            {
                return Ok(stock);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = stockDto.ToStockCreateRequestDTO();
            await _stockRepo.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.ToStockDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await _stockRepo.UpdateStockAsync(id, stockDTO);
            if (stockModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stockModel.ToStockDTO());
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await _stockRepo.DeleteStockAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
