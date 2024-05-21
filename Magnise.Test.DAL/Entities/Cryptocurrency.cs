
namespace Magnise.Test.DAL.Entities
{
    public class Cryptocurrency
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal WorthInUSD { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
