using MailGraphAnalysis.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Services
{
    public interface ICoinService
    {
        Task<ICollection<CoinDto>> GetCoinsAllPreviousInformationAsync();
        Task<ICollection<CoinRateDto>> GetByIdAsync(int id);
        Task UpdateByCoinIdAsync(int id);
        Task DeleteAsync(int id);
        Task AddAsync(CoinDto fullCoinDto);
    }
}
