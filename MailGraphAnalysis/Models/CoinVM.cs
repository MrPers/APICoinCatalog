using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Models
{
    public class CoinVM
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlIcon { get; set; }
    }
}
