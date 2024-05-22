
using System.Text.Json.Serialization;

namespace Magnise.Test.BL.DTO
{
    public class CryptocurrencyDTO
    {
        [JsonPropertyName("asset_id")]
        public string AssetID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price_usd")]
        public decimal PriceInUSD { get; set; }

        [JsonPropertyName("type_is_crypto")]
        public int IsCrypto { get; set; }

        [JsonPropertyName("data_trade_end")]
        public DateTime LastUpdate { get; set; }
    }
}
