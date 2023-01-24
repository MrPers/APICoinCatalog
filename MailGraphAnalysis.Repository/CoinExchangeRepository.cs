using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.DB;
using MailGraphAnalysis.DB.Models;
using MailGraphAnalysis.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Repository
{
    public class CoinExchangeRepository : BaseRepository<CoinExchange, CoinExchangeDto, int>, ICoinExchangeRepository
    {
        public CoinExchangeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ICollection<CoinExchangeDto>> GetByCoinIdAsync(int id)
        {
            var oldCoinExchanges = await this.PrivateGetByCoinIdAsync(id);

            if (oldCoinExchanges.Count < 1 || oldCoinExchanges == null)
            {
                throw new ArgumentNullException(nameof(oldCoinExchanges));
            }

            return _mapper.Map<ICollection<CoinExchangeDto>>(oldCoinExchanges);
        }

        public async Task UpdateByCoinIdAsync(int Id, IList<CoinExchange> coinExchanges)
        {
            if (coinExchanges.Count < 1 || coinExchanges == null)
            {
                throw new ArgumentNullException(nameof(coinExchanges));
            }

            List<CoinExchange> oldCoinExchanges = await this.PrivateGetByCoinIdAsync(Id);

            if (oldCoinExchanges.Count < 1 || oldCoinExchanges == null)
            {
                throw new ArgumentNullException(nameof(oldCoinExchanges));
            }

            for (int i = 0; i < oldCoinExchanges.Count; i++)
            {
                _context.Entry(coinExchanges[i])
                    .CurrentValues
                    .SetValues(oldCoinExchanges[i].Id);
            }

            await _context.SaveChangesAsync();
        }

        private async Task<List<CoinExchange>> PrivateGetByCoinIdAsync(int id)
        {
            var oldCoinExchanges = await _context.CoinExchanges
                .Where(t => t.CoinId == id)
                .ToListAsync();

            return oldCoinExchanges;
        }
    }
}
