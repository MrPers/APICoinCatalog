using Сoin.DTO;
using Сoin.Entity.DB;
using Base.Contracts;

namespace Base.Data
{
    public interface ICoinRepository : IBaseRepository<Coin, CoinDto, int>
    {
        Task<ICollection<CoinDto>> GetCoinsAllWithPreviousInformationAsync();
        Task<CoinDto> GetCoinsAllFullInformationAsync(int id);
    }
}
