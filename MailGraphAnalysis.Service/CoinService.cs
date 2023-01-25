using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.Data.Business;
using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Services
{
    public class CoinService: ICoinService
    {
        private readonly IMapper _mapper;
        private readonly ICoinRepository _coinRepository;
        private readonly ICoinExchangeRepository _coinExchangeRepository;

        public CoinService(ICoinRepository dispatchRepository, ICoinExchangeRepository coinExchangeRepository, IMapper mapper)
        {
            _coinExchangeRepository = coinExchangeRepository;
            _coinRepository = dispatchRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CoinDto>> GetAllAsync()
        {
            var coins = await _coinRepository.GetAllAsync();

            return coins;
        }

        public async Task<ICollection<CoinsWithPreviousInformationDto>> GetCoinsAllWithPreviousInformation()
        {
            var coins = await _coinRepository.GetCoinsAllWithPreviousInformation();

            return coins;
        }

        public async Task<ICollection<CoinExchangeDto>> GetByIdAsync([Range(1, int.MaxValue)] int id)
        {
            var coin = await _coinExchangeRepository.GetByCoinIdAsync(id);

            return coin;
        }

        public async Task AddAsync(CoinDto coinDto)
        {
            if (coinDto == null)
            {
                throw new ArgumentNullException(nameof(coinDto));
            }

            var coins = await _coinRepository.AddAsync(new List<CoinDto> { coinDto });

            try
            {
                var value = await CoinFromAPI.TakeCoinsFromAPIAsync(coins);
                await _coinExchangeRepository.AddAsync(value);
            }
            catch (Exception ex)
            {
                await _coinRepository.DeleteAsync(coins.First().Id);

                throw new Exception(ex.Message);
            }

        }

        public async Task UpdateByCoinIdAsync([Range(1, int.MaxValue)] int id)
        {
            var coin = await _coinRepository.GetByIdAsync(id);
            var coinResult = _mapper.Map<Coin>(coin);
            try
            {
                var values = await CoinFromAPI.TakeCoinsFromAPIAsync(new List<Coin> { coinResult });
                await _coinExchangeRepository.UpdateByCoinIdAsync(id, values);
            }
            catch (Exception)
            {
                await _coinRepository.DeleteAsync(id);
            }
        }

        public async Task DeleteAsync([Range(1, int.MaxValue)] int id)
        {
            await _coinRepository.DeleteAsync(id);
        }
    }
}
