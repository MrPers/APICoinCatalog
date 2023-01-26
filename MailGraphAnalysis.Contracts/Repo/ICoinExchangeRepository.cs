using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Repo
{
    public interface ICoinExchangeRepository : IBaseRepository<CoinRate, CoinRateDto, int>
    {
        Task<ICollection<CoinRateDto>> GetByCoinIdAsync(int id);
        Task UpdateByCoinIdAsync(int id, IList<CoinRate> coinExchanges);
    }
}
