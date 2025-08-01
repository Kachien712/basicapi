﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Extensions;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return BadRequest("Stock not found.");
            }

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            if (userPortfolio.Any(s => s.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio.");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _portfolioRepo.CreatePortfolioAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create.");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete (string symbol)
        {
            var user = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(user);

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolioAsync(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock is not in the portfolio.");
            }

            return Ok();
        }
    }
}
