using MailGraphAnalysis.Persistence;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailGraphAnalysis.Entity
{
    public class CoinExchange : BaseEntity<int>
    {
        public float Prices { get; set; }
        public float VolumeTraded { get; set; }
        public DateTime Time { get; set; }
        public int CoinId { get; set; }
        [ForeignKey("CoinId")]
        [Required]
        public Coin? Coin { get; set; }

        public CoinExchange(float prices, float volumeTraded, DateTime time, int coinId)
        {
            Prices = prices;
            VolumeTraded = volumeTraded;
            Time = time;
            CoinId = coinId;
        }
    }
}