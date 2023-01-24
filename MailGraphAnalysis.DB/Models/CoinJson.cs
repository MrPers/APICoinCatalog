using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.DB.Models
{
    public class CoinExchangeJson
    {
        [JsonProperty("time_period_end")]
        public DateTime Time;
        [JsonProperty("price_high")]
        public double PriceHigh;
        [JsonProperty("price_low")]
        public double PriceLow;
        [JsonProperty("volume_traded")]
        public double VolumeTraded;
    }
}
