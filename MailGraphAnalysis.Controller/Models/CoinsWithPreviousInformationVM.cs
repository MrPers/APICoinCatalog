using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Controllers.Models
{
    public class CoinsWithPreviousInformationVM
    {
        [Required]
        public int Id { get; set; }
        [MinLength(4)]
        public string Name { get; set; }
        public float Prices { get; set; }
        public float VolumeTraded { get; set; }
    }
}
