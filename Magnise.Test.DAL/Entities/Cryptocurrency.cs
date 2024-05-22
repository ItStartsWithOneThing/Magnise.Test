
namespace Magnise.Test.DAL.Entities
{
    public class Cryptocurrency
    {
        public int ID { get; set; }
        public string AssetID { get; set; }
        public string Name { get; set; }
        public decimal PriceInUSD { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
