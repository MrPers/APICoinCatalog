using MailGraphAnalysis.Persistence;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailGraphAnalysis.Entity
{
    public class CoinRate : BaseEntity<int>
    {
        public DateTime Time;
        public float VolumeTraded;
        public float Prices { get; set; }
        public int CoinId { get; set; }
        [ForeignKey("CoinId")]
        [Required]
        public Coin? Coin { get; set; }

    }
}