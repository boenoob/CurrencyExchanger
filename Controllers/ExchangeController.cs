using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyExchanger.Dtos;
using CurrencyExchanger.Entities;
using CurrencyExchanger.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchanger.Controllers {
 
    [ApiController]
    [Route("exchange")]
    public class ExchangeController : ControllerBase 
    {
        private readonly ExchangesInterface repository;

        public ExchangeController(ExchangesInterface repository) 
        {
            this.repository = repository;
        }

        // GET /exchange
        [HttpGet]
        public IEnumerable<RateDto> GetExchangeRates()
        {
            repository.StoreRatesFromFixer();
            var rates = repository.GetExchangeRates().Select( Rate => Rate.AsDto());
            return rates;
        }

        // GET - 
        [HttpGet("rate")]
        public ActionResult<ExchangeDto> GetExchange2(string currency1, string currency2)
        {
            double rate = repository.GetRate(currency1, currency2);
            Exchange exchange = new() 
            {
                Date = DateTimeOffset.Now,
                Currency1 = currency1,
                Currency2 = currency2,
                Amount = 1,
                ExchangeRate = rate,
                ConvertResult = 1 * rate
            };
            if (rate < 0) return NotFound();
            return exchange.AsDto();
        }

        [HttpGet("convert")]
        public ActionResult<ExchangeDto> GetExchange3(string currency1, string currency2, double amount)
        {
            double rate = repository.GetRate(currency1, currency2); // GET RATE FROM REPO

            if (rate < 0) return NotFound();
            
            Exchange exchange = new() 
            {
                Date = DateTimeOffset.Now,
                Currency1 = currency1,
                Currency2 = currency2,
                Amount = amount,
                ExchangeRate = rate,
                ConvertResult = amount * rate
            };
            return exchange.AsDto();
        }
    }
}