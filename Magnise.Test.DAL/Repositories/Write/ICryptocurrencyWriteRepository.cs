
using Magnise.Test.DAL.Entities;

namespace Magnise.Test.DAL.Repositories.Write
{
    public interface ICryptocurrencyWriteRepository
    {
        public Task AddCurrencyAsync(Cryptocurrency currency);

        public Task UpdateCurrencyAsync(Cryptocurrency currency);
    }
}
