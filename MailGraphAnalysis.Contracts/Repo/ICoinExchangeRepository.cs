using MailGraphAnalysis.DB.Models;
using MailGraphAnalysis.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Repo
{
    public interface ICoinExchangeRepository : IBaseRepository<CoinExchange, CoinExchangeDto, int>
    {
        Task<ICollection<CoinExchangeDto>> GetByCoinIdAsync(int id);
        Task UpdateByCoinIdAsync(int id, IList<CoinExchange> coinExchanges);
    }
}
