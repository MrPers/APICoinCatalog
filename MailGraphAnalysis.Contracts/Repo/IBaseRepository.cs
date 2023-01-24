using MailGraphAnalysis.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Repo
{
    public interface IBaseRepository<TTable, TDto, TId> where TTable : IBaseEntity<TId>
    {
        Task<ICollection<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(TId Id);
        Task<ICollection<TTable>> AddAsync(ICollection<TDto> dto);
        Task<ICollection<TTable>> AddAsync(ICollection<TTable> table);
        Task<TTable> AddAsync(TDto dto);
        Task UpdateAsync(TId Id, TDto meaning);
        Task DeleteAsync(TId Id);
        //Task SaveChangesAsync();
    }
}

