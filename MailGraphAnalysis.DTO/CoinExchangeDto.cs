using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.DTO
{
    public class CoinExchangeDto : IBaseEntity<int>
    {
        public int Id { get; set; }
        public float Prices { get; set; }
        public float VolumeTraded { get; set; }
        public DateTime Time { get; set; }
    }
}
