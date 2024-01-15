using System;
using Line.Models;
using Line.ModelView;
using Line.Repositories;
using Line.Repositories.Implementations;
using Line.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Line.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController: ControllerBase
	{
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyRequestModel model)
        {
            try
            {
                // Find if given currency is existed in the database

                var existingCurrency = await _currencyRepository.GetCurrencyByName( model.CurrencyName);
                if (existingCurrency != null )
                {
                    return BadRequest("Currency with the same name already exists");
                }

                var currency = new Currency
                {
                    CurrencyName = model.CurrencyName,
                    CurrencyAbbreviation = model.CurrencyAbbreviation,
                };

                var createdCurrency = await _currencyRepository.Create(currency);

                if (createdCurrency != null)
                {
                    var responseCurrency = new
                    {
                        createdCurrency.CurrencyId,
                        createdCurrency.CurrencyName,
                        createdCurrency.CurrencyAbbreviation
                    };
                    return Ok(new { success = true, currency = createdCurrency });
                }
                else
                {
                    return BadRequest("Invalid currency");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the currency: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrency(int id)
        {
            try
            {
                var currency = await _currencyRepository.GetById(id);
                if (currency == null)
                    return NotFound();

                return Ok(currency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the currency: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var currencies = await _currencyRepository.GetAll();
            var currencyData = currencies.Select(crn => new
            {
                currencyName = crn.CurrencyName,
                currencyId = crn.CurrencyId,
                CurrencyAbbreviation = crn.CurrencyAbbreviation,
            });
            return Ok(currencyData);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, Currency currency)
        {
            try
            {
                var existingCurrency = await _currencyRepository.GetById(id);
                if (existingCurrency == null)
                    return NotFound();

                existingCurrency.CurrencyName = currency.CurrencyName;
                existingCurrency.CurrencyAbbreviation = currency.CurrencyAbbreviation;
                // Update other properties as needed

                await _currencyRepository.Update(existingCurrency);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the currency: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            try
            {
                await _currencyRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the currency: {ex.Message}");
            }
        }
    }
}

