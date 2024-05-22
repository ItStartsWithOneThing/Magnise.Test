
using Magnise.Test.BL.DTO.API;

namespace Magnise.Test.BL.Services
{
    public interface ICryptocurrencyService
    {
        public Task<IEnumerable<CryptocurrencyShortResponse>> GetAllCryptoCurrencies();
    }
}
