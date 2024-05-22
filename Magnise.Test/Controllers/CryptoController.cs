
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
        /// <response code="500">Internal server error</response>
        [HttpGet("get-all-supported")]
        [ProducesResponseType(typeof(IEnumerable<CryptocurrencyShortResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCryptoCurrencies()
        {
            var result = await _cryptocurrencyService.GetAllCryptoCurrencies();

            return result != null ? Ok(result) : BadRequest("Failed to fetch currencies");
        }

        /// <summary>
        /// Get all crypto currency by providing id
        /// </summary>
        /// <response code="200"></response>
        /// <response code="400">Failed to fetch currency</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(typeof(CryptocurrencyFullResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _cryptocurrencyService.GetById(id);

            return result != null ? Ok(result) : BadRequest("Failed to fetch currency");
        }

        /// <summary>
        /// Get collection of crypto currencies by providing colelction of ids
        /// </summary>
        /// <response code="200"></response>
        /// <response code="400">Failed to fetch currencies</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("get-collection")]
        [ProducesResponseType(typeof(IEnumerable<CryptocurrencyFullResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetConnectionByIds([FromBody] List<int> ids)
        {
            var result = await _cryptocurrencyService.GetConnectionByIds(ids);

            return result != null ? Ok(result) : BadRequest("Failed to fetch currency");
        }
    }
}
