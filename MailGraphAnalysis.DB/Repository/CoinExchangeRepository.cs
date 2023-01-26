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

namespace MailGraphAnalysis.Data.Repository
{
    public class CoinExchangeRepository : BaseRepository<CoinRate, CoinRateDto, int>, ICoinExchangeRepository
    {
        public CoinExchangeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ICollection<CoinRateDto>> GetByCoinIdAsync(int id)
        {
            var oldCoinRate = await this.PrivateGetByCoinIdAsync(id);

            if (oldCoinRate.Count < 1 || oldCoinRate == null)
            {
                throw new ArgumentNullException(nameof(oldCoinRate));
            }

            return _mapper.Map<ICollection<CoinRateDto>>(oldCoinRate);
        }

        public async Task UpdateByCoinIdAsync(int Id, IList<CoinRate> coinExchanges)
        {
            if (coinExchanges.Count < 1 || coinExchanges == null)
            {
                throw new ArgumentNullException(nameof(coinExchanges));
            }

            List<CoinRate> oldCoinRate = await this.PrivateGetByCoinIdAsync(Id);

            if (oldCoinRate.Count < 1 || oldCoinRate == null)
            {
                throw new ArgumentNullException(nameof(oldCoinRate));
            }

            for (int i = 0; i < oldCoinRate.Count; i++)
            {
                _context.Entry(coinExchanges[i])
                    .CurrentValues
                    .SetValues(oldCoinRate[i].Id);
            }

            await _context.SaveChangesAsync();
        }

        private async Task<List<CoinRate>> PrivateGetByCoinIdAsync(int id)
        {
            var oldCoinRate = await _context.CoinRate
                .Where(t => t.CoinId == id)
                .ToListAsync();

            return oldCoinRate;
        }
    }
}
