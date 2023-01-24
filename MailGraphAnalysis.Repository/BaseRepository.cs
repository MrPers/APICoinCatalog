using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.DB;
using MailGraphAnalysis.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Repository
{
    public abstract class BaseRepository<TTable, TDto, TId> : IBaseRepository<TTable, TDto, TId> where TTable : class, IBaseEntity<TId>
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;
        public BaseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TTable> AddAsync(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var time = _mapper.Map<TTable>(dto);

            if (time == null)
            {
                throw new ArgumentNullException(nameof(time));
            }

            await _context.Set<TTable>().AddAsync(time);

            await _context.SaveChangesAsync();

            return time;
        }

        public async Task<ICollection<TTable>> AddAsync(ICollection<TDto> dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var times = _mapper.Map<ICollection<TTable>>(dto);

            if (times == null)
            {
                throw new ArgumentNullException(nameof(times));
            }

            await _context.Set<TTable>().AddRangeAsync(times);

            await _context.SaveChangesAsync();

            return times;
        }
        public async Task<ICollection<TTable>> AddAsync(ICollection<TTable> table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            await _context.Set<TTable>().AddRangeAsync(table);

            await _context.SaveChangesAsync();

            return table;
        }

        public async Task UpdateAsync(TId Id, TDto meaning)
        {
            if (meaning == null)
            {
                throw new ArgumentNullException(nameof(meaning));
            }

            var result = await _context.Set<TTable>().FindAsync(Id);

            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            _context.Entry(result)
                .CurrentValues
                .SetValues(_mapper.Map<TTable>(meaning));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId Id)
        {
            var obj = await _context.Set<TTable>().FindAsync(Id);
            
            if (obj != null)
            {
                _context.Entry<TTable>(obj).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

        }

        public async Task<ICollection<TDto>> GetAllAsync()
        {
            var dbItems = await _context.Set<TTable>().ToListAsync();
            return _mapper.Map<ICollection<TDto>>(dbItems);
        }

        public async Task<TDto> GetByIdAsync(TId Id)
        {
            var dbItem = await _context.Set<TTable>().FindAsync(Id);
            return _mapper.Map<TDto>(dbItem);
        }

    }
}
