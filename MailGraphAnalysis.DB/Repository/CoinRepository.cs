using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Data;
using MailGraphAnalysis.DTO;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailGraphAnalysis.Entity;

namespace MailGraphAnalysis.Data.Repository
{
    public class CoinRepository : BaseRepository<Coin, CoinDto, int>, ICoinRepository
    {
        public CoinRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ICollection<CoinDto>> GetCoinsAllWithPreviousInformationAsync()
        {
            ICollection<CoinDto> productsDto = new List<CoinDto>();

            foreach (var item in await _context.Coins.ToListAsync())
            {
                var productDto = await _context.Coins
                    .Join(_context.CoinRate
                    .Where(x => x.CoinId == item.Id)
                    .Where(t => t.Time == _context.CoinRate
                        .Where(p => p.CoinId == item.Id).Max(v => v.Time)),
                    p => p.Id,
                    t => t.CoinId,
                    (p, t) => new CoinDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Prices = t.Prices,
                        VolumeTraded = t.VolumeTraded,
                    }
                    )
                .FirstAsync();

                productsDto.Add(productDto);
            }

            return productsDto;
        }

        public async Task<ICollection<Coin>> GetCoinsFindByNameAsync(IEnumerable<String> names)
        {
            ICollection<Coin> productsDto = new List<Coin>();

            foreach (var item in names)
            {
                var productDto = await _context.Coins
                    .Where(p => p.Name == item).FirstAsync();

                productsDto.Add(productDto);
            }

            return productsDto;
        }
    }
}
