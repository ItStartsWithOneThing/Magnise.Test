
using Magnise.Test.BL.DTO.API;
using Magnise.Test.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Magnise.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CryptoController : ControllerBase
    {
        private readonly ILogger<CryptoController> _logger;
        private readonly ICryptocurrencyService _cryptocurrencyService;

        public CryptoController(
            ILogger<CryptoController> logger,
            ICryptocurrencyService cryptocurrencyService)
        {
            _logger = logger;
            _cryptocurrencyService = cryptocurrencyService;
        }

        /// <summary>
        /// Get all supported crypto currencies
        /// </summary>
        /// <response code="200"></response>
        /// <response code="400">Failed to fetch currencies</response>
        [HttpGet("all-cryptocurrencies")]
        [ProducesResponseType(typeof(IEnumerable<CryptocurrencyShortResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCryptoCurrencies()
        {
            var result = await _cryptocurrencyService.GetAllCryptoCurrencies();

            return result != null ? Ok(result) : BadRequest("Failed to fetch currencies");
        }
    }
}
