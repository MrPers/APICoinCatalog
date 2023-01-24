using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Controllers.Models
{
    public class CoinVM
    {
        [Required]
        public int Id { get; set; }
        [MinLength(4)]
        public string Name { get; set; }
    }
}
