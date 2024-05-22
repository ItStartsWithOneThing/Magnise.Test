
using Magnise.Test.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Magnise.Test.DAL.Repositories.Write
{
    public class CryptocurrencyWriteRepository : ICryptocurrencyWriteRepository
    {
        protected readonly CryptoDBContext _cryptoDBContext;
        protected readonly DbSet<Cryptocurrency> _dbSet;

        public CryptocurrencyWriteRepository(
            CryptoDBContext context)
        {
            _cryptoDBContext = context;
            _dbSet = context.Set<Cryptocurrency>();
        }

        public async Task InitCurrencies(IEnumerable<Cryptocurrency> currencies)
        {
            await _dbSet.AddRangeAsync(currencies);

            await Save();
        }

        public async Task AddCurrencyAsync(Cryptocurrency currency)
        {
            await _dbSet.AddAsync(currency);

            await Save();
        }

        public async Task UpdateCurrencyPriceAsync(Cryptocurrency currency)
        {
            await _dbSet
                .Where(x => x.ID == currency.ID)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.PriceInUSD, x => currency.PriceInUSD)
                    .SetProperty(x => x.LastUpdate, x => currency.LastUpdate)
                    );

            await Save();
        }

        private async Task<int> Save()
        {
            return await _cryptoDBContext.SaveChangesAsync();
        }
    }
}
