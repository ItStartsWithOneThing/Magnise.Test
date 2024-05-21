
using Magnise.Test.DAL.Entities;

namespace Magnise.Test.DAL.Repositories.Read
{
    public interface ICryptocurrencyReadRepository
    {
        public Task<Cryptocurrency> GetCurrencyByIDAsync(int id);
        public Task<IEnumerable<Cryptocurrency>> GetAllCurrenciesAsync();
        public Task<IEnumerable<Cryptocurrency>> GetCurrenciesCollectionByIDAsync(List<int> ids);
    }
}
