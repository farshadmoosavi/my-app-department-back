using System;
using Line.Models;
using Line.Repositories.Implementations;
using Line.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Line.ModelView;
using System.Globalization;
using Castle.Core.Resource;


namespace Line.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellBuyPriceController : ControllerBase
    {
        private readonly ISellBuyPriceRepository _sellBuyPriceRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public SellBuyPriceController(ISellBuyPriceRepository sellBuyPriceRepository, ICurrencyRepository currencyRepository)
        {
            _sellBuyPriceRepository = sellBuyPriceRepository;
            _currencyRepository = currencyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSellBuyPrice(SellBuyPrice sellBuyPrice)
        {

            try
            {
                // Create the new document
                await _sellBuyPriceRepository.Create(sellBuyPrice);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the sellBuyPrice: {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSellBuyPrice(int id)
        {
            try
            {
                var sellBuyPrice = await _sellBuyPriceRepository.GetById(id);
                if (sellBuyPrice == null)
                    return NotFound();

                return Ok(sellBuyPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the document: {ex.Message}");
            }
        }

        [HttpGet("NondeletedSellBuyPrices")]
        public async Task<IActionResult> GetAllNonDeletedSellBuyPrices()
        {
            var sellBuyPrices = await _sellBuyPriceRepository.GetAllNonDeletedSellBuyPrices();
            var sortedSellBuyPrices = sellBuyPrices.OrderBy(sbp => sbp.LastUpdate).ToList(); // Change to OrderBy
            return Ok(sortedSellBuyPrices);
        }

        [HttpGet("deletedSellBuyPrices")]
        public async Task<IActionResult> GetAllDeletedSellBuyPrices()
        {
            var sellBuyPrices = await _sellBuyPriceRepository.GetAllDeletedSellBuyPrices();
            var sortedSellBuyPrices = sellBuyPrices.OrderBy(sbp => sbp.LastUpdate).ToList(); // Change to OrderBy
            return Ok(sortedSellBuyPrices);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSellBuyPrice(int id, SellBuyPrice sellBuyPrice)
        {
            try
            {
                var existingSellBuyPrice = await _sellBuyPriceRepository.GetById(id);
                if (existingSellBuyPrice == null)
                    return NotFound();

                existingSellBuyPrice.SellPrice = sellBuyPrice.SellPrice;
                existingSellBuyPrice.BuyPrice = sellBuyPrice.BuyPrice;
                if (sellBuyPrice.currencyId != existingSellBuyPrice.currencyId)
                {
                    // Retrieve the new currency object based on sellBuyPrice.currencyId
                    var newCurrency = await _currencyRepository.GetById(sellBuyPrice.currencyId);
                    if (newCurrency != null)
                    {
                        existingSellBuyPrice.currencies = newCurrency;
                        existingSellBuyPrice.currencyId = sellBuyPrice.currencyId;
                    }
                    else
                    {
                        return NotFound($"Currency with ID {sellBuyPrice.currencyId} not found.");
                    }
                }

                existingSellBuyPrice.LastUpdate = DateTime.Now; // Update the LastUpdate property

                await _sellBuyPriceRepository.Update(existingSellBuyPrice);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the document: {ex.Message}");
            }
        }
    }
}
