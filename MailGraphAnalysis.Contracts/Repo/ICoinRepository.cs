using MailGraphAnalysis.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailGraphAnalysis.Entity.DB;

namespace MailGraphAnalysis.Contracts.Repo
{
    public interface ICoinRepository : IBaseRepository<Coin, CoinDto, int>
    {
        Task<ICollection<CoinDto>> GetCoinsAllWithPreviousInformationAsync();
        Task<CoinDto> GetCoinsAllFullInformationAsync(int id);
    }
}
