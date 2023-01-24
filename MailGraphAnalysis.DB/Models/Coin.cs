using MailGraphAnalysisGraphAnalysis.DB.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailGraphAnalysis.DB.Models
{
    //[Index(nameof(Name), IsUnique = true)]
    public class Coin : BaseEntity<int>
    {
        [JsonProperty("asset_id")]
        [Column(TypeName = "varchar(5)")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string UrlIcon { get; set; }

        public bool AddedCoin { get; set; }
        public ICollection<CoinExchange> CoinExchanges { get; set; } = new List<CoinExchange>();
    }
}