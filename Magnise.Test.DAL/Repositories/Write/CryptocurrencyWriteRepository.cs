
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

        public async Task AddCurrencyAsync(Cryptocurrency currency)
        {
            await _dbSet.AddAsync(currency);

            await _cryptoDBContext.SaveChangesAsync();
        }

        public async Task UpdateCurrencyAsync(Cryptocurrency currency)
        {
            _cryptoDBContext.Entry(currency).State = EntityState.Modified;

            await _cryptoDBContext.SaveChangesAsync();
        }
    }
}
