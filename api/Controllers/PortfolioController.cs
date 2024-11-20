using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<User> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio() {
            var username = User.GetUsername();
            var user  = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol) {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null) { return BadRequest("Stock not found"); }

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);

            // Cannot add same stock twice
            if (userPortfolio.Any(e => e.Symbol.ToLower() ==  symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            // Create function
            var portfolioModel = new Portfolio {
                StockId = stock.Id,
                UserId = user.Id
            };
            await _portfolioRepo.CreatePortfolio(portfolioModel);

            if (portfolioModel == null) return StatusCode(500, "Could not create");
            else return Created();
        }
    }
}