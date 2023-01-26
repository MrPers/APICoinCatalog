using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Entity
{
    public class CoinJSON
    {
        [JsonProperty("symbol")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public Description Description { get; set; }
        [JsonProperty("image")]
        public Image URLImage { get; set; }
        [JsonProperty("genesis_date")]
        public DateTime? GenesisDate { get; set; }
    }

    public partial class Description
    {
        [JsonProperty("en")]
        public string En { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("thumb")]
        public string Thumb { get; set; }
    }
    public class CoinRateJSON
    {
        [JsonProperty("time_period_end")]
        public DateTime Time;
        [JsonProperty("price_high")]
        public float PriceHigh;
        [JsonProperty("price_low")]
        public float PriceLow;
        [JsonProperty("volume_traded")]
        public float VolumeTraded;
    }
}
