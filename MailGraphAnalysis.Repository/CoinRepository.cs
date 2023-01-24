using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.DB;
using MailGraphAnalysis.DB.Models;
using MailGraphAnalysis.DTO;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Repository
{
    public class CoinRepository : BaseRepository<Coin, CoinDto, int>, ICoinRepository
    {
        public CoinRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ICollection<CoinsWithPreviousInformationDto>> GetCoinsAllWithPreviousInformation()
        {
            ICollection<CoinsWithPreviousInformationDto> productsDto = new List<CoinsWithPreviousInformationDto>();

            foreach (var item in await _context.Coins.ToListAsync())
            {
                var productDto = await _context.Coins
                    .Join(_context.CoinExchanges
                    .Where(x => x.CoinId == item.Id)
                    .Where(t => t.Time == _context.CoinExchanges
                        .Where(p => p.CoinId == item.Id).Max(v => v.Time)),
                    p => p.Id,
                    t => t.CoinId,
                    (p, t) => new CoinsWithPreviousInformationDto
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
    }
}
