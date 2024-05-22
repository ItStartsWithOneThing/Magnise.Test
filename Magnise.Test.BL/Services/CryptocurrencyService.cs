
using AutoMapper;
using Magnise.Test.BL.DTO.API;
using Magnise.Test.DAL.Repositories.Read;

namespace Magnise.Test.BL.Services
{
    public class CryptocurrencyService : ICryptocurrencyService
    {
        private readonly IMapper _mapper;
        private readonly ICryptocurrencyReadRepository _readRepository;

        public CryptocurrencyService(
            IMapper mapper,
            ICryptocurrencyReadRepository readRepository)
        {
            _mapper = mapper;
            _readRepository = readRepository;
        }

        public async Task<IEnumerable<CryptocurrencyShortResponse>> GetAllCryptoCurrencies()
        {
            var dbCurrencies = await _readRepository.GetAllCurrenciesAsync();

            return _mapper.Map<IEnumerable<CryptocurrencyShortResponse>>(dbCurrencies);
        }

        public async Task<CryptocurrencyFullResponse> GetById(int id)
        {
            var dbCurrency = await _readRepository.GetCurrencyByIDAsync(id);

            return _mapper.Map<CryptocurrencyFullResponse>(dbCurrency);
        }

        public async Task<IEnumerable<CryptocurrencyFullResponse>> GetConnectionByIds(List<int> ids)
        {
            var dbCurrencies = await _readRepository.GetCurrenciesCollectionByIDAsync(ids);

            return _mapper.Map<IEnumerable<CryptocurrencyFullResponse>>(dbCurrencies);
        }
    }
}
