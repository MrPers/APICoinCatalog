using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.DTO
{
    public class CoinRateDto : IBaseEntity<int>
    {
        public int Id { get; set; }
        public double Prices { get; set; }
        public double VolumeTraded { get; set; }
        public DateTime Time { get; set; }
    }
}
