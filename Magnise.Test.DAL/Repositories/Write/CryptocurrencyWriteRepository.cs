
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
            _dbSet.Attach(currency);

            _cryptoDBContext.Entry(currency).Property(c => c.PriceInUSD).IsModified = true;
            _cryptoDBContext.Entry(currency).Property(c => c.LastUpdate).IsModified = true;

            await Save();
        }

        private async Task<int> Save()
        {
            return await _cryptoDBContext.SaveChangesAsync();
        }
    }
}
