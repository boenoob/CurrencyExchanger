using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyExchanger.Dtos;
using CurrencyExchanger.Entities;
using CurrencyExchanger.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CurrencyExchanger.Controllers {
 
    [ApiController]
    [Route("exchange")]
    public class ExchangeController : ControllerBase 
    {
        private readonly ExchangesInterface repository;
        private readonly IConfiguration _configuration;
        
        public ExchangeController(ExchangesInterface repository, IConfiguration configuration) 
        {
            this.repository = repository;
            _configuration = configuration;
            this.repository.SetApiKey(_configuration.GetValue<string>("ApiKey"));
        }

        // GET /exchange
        [HttpGet]   
        public IEnumerable<RateDto> GetExchangeRates()
        {
            repository.StoreRatesFromFixer();
            var rates = repository.GetExchangeRates().Select( Rate => Rate.AsDto());
            return rates;
        }

        // GET /rate
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
            if (rate < 0) return BadRequest("Invalid currency");;
            return exchange.AsDto();
        }

        [HttpGet("convert")]
        public ActionResult<ExchangeDto> GetExchange3(string currency1, string currency2, double amount)
        {
            double rate = repository.GetRate(currency1, currency2); // GET RATE FROM REPO

            if (rate < 0) return BadRequest("Invalid currency");
            
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