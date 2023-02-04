using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Data;
using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailGraphAnalysis.Entity.DB;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Data.Repository
{
    public class CoinExchangeRepository : BaseRepository<CoinRate, CoinRateDto, int>, ICoinExchangeRepository
    {
        public CoinExchangeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ICollection<CoinRateDto>> GetCoinRateAllByIdAsync([Range(1, int.MaxValue)] int id, [Range(24, int.MaxValue)] int step)
        {

            var oldCoinRate = await this.PrivateGetByCoinIdAsync(id, step);

            if (oldCoinRate.Count < 1 || oldCoinRate == null)
            {
                throw new ArgumentNullException(nameof(oldCoinRate));
            }

            return _mapper.Map<ICollection<CoinRateDto>>(oldCoinRate);
        }

        public async Task<DateTime> GetLastCoinRepositoryAsync([Range(0, long.MaxValue)] int id)
        {
            var result = _context.CoinRate
                .Where(p => p.CoinId == id).OrderByDescending(x => x.Time)
              .FirstOrDefault();

            return result.Time;
        }

        private async Task<ICollection<CoinRate>> PrivateGetByCoinIdAsync([Range(0, long.MaxValue)] int id, [Range(24, int.MaxValue)] int step)
        {
            var oldCoinRate = await _context.GetCoins(id, step).ToListAsync(); ;

            return oldCoinRate;
        }
    }
}
