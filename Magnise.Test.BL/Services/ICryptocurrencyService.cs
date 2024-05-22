
using Magnise.Test.BL.DTO.API;

namespace Magnise.Test.BL.Services
{
    public interface ICryptocurrencyService
    {
        public Task<IEnumerable<CryptocurrencyShortResponse>> GetAllCryptoCurrencies();
        public Task<CryptocurrencyFullResponse> GetById(int id);
        public Task<IEnumerable<CryptocurrencyFullResponse>> GetConnectionByIds(List<int> ids);
    }
}
