using MailGraphAnalysis.DTO;
using MailGraphAnalysis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Persistence
{
    public interface ICoinFromAPI
    {
        Task<IList<Coin>> TakeCoinsNameFromAPIAsync(ICollection<string> names);
        Task<IList<CoinRate>> TakeCoinsFromAPIAsync(ICollection<CoinDto> names);
    }
}
