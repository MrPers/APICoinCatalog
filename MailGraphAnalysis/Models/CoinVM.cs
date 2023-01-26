using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Models
{
    public class CoinVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlIcon { get; set; }
        public float Prices { get; set; }
        public float VolumeTraded { get; set; }
    }
}
