using System.ComponentModel.DataAnnotations;

namespace Сoin.Api.Models
{
    public class CoinRateVM
    {
        public double Prices { get; set; }
        public double VolumeTraded { get; set; }
        public long Time { get; set; }
    }
}
