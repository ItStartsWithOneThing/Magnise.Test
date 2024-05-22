
namespace Magnise.Test.BL.DTO.API
{
    public class CryptocurrencyFullResponse
    {
        public int ID { get; set; }
        public string AssetID { get; set; }
        public string Name { get; set; }
        public decimal PriceInUSD { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
