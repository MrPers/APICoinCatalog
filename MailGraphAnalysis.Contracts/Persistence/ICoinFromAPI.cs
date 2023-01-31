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
        Task<CoinDto> TakeCoinNameFromAPIAsync(string name);
        Task<IList<CoinRateDto>> TakeCoinsFromAPIAsync(string name, DateTime date);
    }
}
