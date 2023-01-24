using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Models
{
    public class CoinVM
    {
        [Required]
        public int Id { get; set; }
        [MinLength(4)]
        public string Name { get; set; }
    }
}
