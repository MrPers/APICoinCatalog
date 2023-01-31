using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailGraphAnalysis.Entity.DB
{
    //[Index(nameof(Name), IsUnique = true)]
    public class Coin : BaseEntity<int>
    {
        public string Name { get; set; }
        public DateTime? GenesisDate { get; set; }
        public string Description { get; set; }
        public string UrlIcon { get; set; }
        public ICollection<CoinRate> CoinRate { get; set; } = new List<CoinRate>();
    }

}
