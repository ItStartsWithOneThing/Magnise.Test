
using Magnise.Test.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Magnise.Test.DAL.Repositories.Read
{
    public class CryptocurrencyReadRepository : ICryptocurrencyReadRepository
    {
        protected readonly CryptoDBContext _cryptoDBContext;
        protected readonly DbSet<Cryptocurrency> _dbSet;

        public CryptocurrencyReadRepository(
            CryptoDBContext context)
        {
            _cryptoDBContext = context;
            _dbSet = context.Set<Cryptocurrency>();
        }

        public async Task<IEnumerable<Cryptocurrency>> GetAllCurrenciesAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Cryptocurrency>> GetCurrenciesCollectionByIDAsync(List<int> ids)
        {
            return await _dbSet.Where(c => ids.Contains(c.ID)).ToListAsync();
        }

        public async Task<Cryptocurrency> GetCurrencyByIDAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Cryptocurrency> GetCurrencyByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
