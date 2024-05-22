
using System.Text.Json.Serialization;

namespace Magnise.Test.BL.DTO
{
    public class CryptocurrencyUpdateDTO
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }

        [JsonPropertyName("symbol_id")]
        public string SymbolID { get; set; }

        [JsonPropertyName("time_coinapi")]
        public DateTime TimeCoinapi { get; set; }
    }
}
