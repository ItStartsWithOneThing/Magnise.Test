
using Magnise.Test.DAL.Entities;

namespace Magnise.Test.DAL.Repositories.Write
{
    public interface ICryptocurrencyWriteRepository
    {
        public Task InitCurrencies(IEnumerable<Cryptocurrency> currencies);

        public Task AddCurrencyAsync(Cryptocurrency currency);

        public Task UpdateCurrencyPriceAsync(Cryptocurrency currency);
    }
}
