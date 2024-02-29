using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CurrencyConverter.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyConvertorController : ControllerBase
    {

        private readonly ILogger<CurrencyConvertorController> _logger;
        private readonly ICurrencyConverterServices _currencyConverterServices;

        public CurrencyConvertorController(ILogger<CurrencyConvertorController> logger, ICurrencyConverterServices currencyConverterServices)
        {
            _logger = logger;
            _currencyConverterServices = currencyConverterServices;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> UpdateConfiguration()
        {
            _currencyConverterServices.UpdateConfiguration(new List<Tuple<string, string, double>>
        {
            Tuple.Create("USD", "CAD", 2.0),
            Tuple.Create("CAD", "GBP",0.25),
            Tuple.Create("USD", "EUR", 10.0)
        });
            return Ok();
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> Convert(string fromCurrency, string toCurrency, double amount)
        {
            double result = _currencyConverterServices.Convert(fromCurrency, toCurrency, amount);
            return Ok(result);
        }

    }
}