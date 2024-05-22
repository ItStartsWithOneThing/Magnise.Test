
using AutoMapper;
using Magnise.Test.BL.DTO.API;
using Magnise.Test.DAL.Repositories.Read;
using Microsoft.Extensions.Logging;

namespace Magnise.Test.BL.Services
{
    public class CryptocurrencyService : ICryptocurrencyService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ICryptocurrencyReadRepository _readRepository;

        public CryptocurrencyService(
            ILogger logger,
            IMapper mapper,
            ICryptocurrencyReadRepository readRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _readRepository = readRepository;
        }

        public async Task<IEnumerable<CryptocurrencyShortResponse>> GetAllCryptoCurrencies()
        {
            var dbCurrencies = await _readRepository.GetAllCurrenciesAsync();

            return _mapper.Map<IEnumerable<CryptocurrencyShortResponse>>(dbCurrencies);
        }
    }
}
