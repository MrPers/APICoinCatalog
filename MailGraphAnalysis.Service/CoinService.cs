using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;
using System.ComponentModel.DataAnnotations;
using MailGraphAnalysis.Contracts.Persistence;

namespace MailGraphAnalysis.Services
{
    public class CoinService: ICoinService
    {
        private readonly ICoinFromAPI _сoinFromAPI;
        private readonly ICoinRepository _coinRepository;
        private readonly ICoinExchangeRepository _coinExchangeRepository;

        public CoinService(ICoinRepository dispatchRepository, ICoinExchangeRepository coinExchangeRepository, ICoinFromAPI сoinFromAPI)
        {
            _coinExchangeRepository = coinExchangeRepository;
            _coinRepository = dispatchRepository;
            _сoinFromAPI = сoinFromAPI;
        }

        public async Task<ICollection<CoinDto>> GetCoinsAllPreviousInformationAsync()
        {
            var coins = await _coinRepository.GetCoinsAllWithPreviousInformationAsync();

            return coins;
        }

        public async Task<ICollection<CoinRateDto>> GetCoinRateAllByIdAsync([Range(1, int.MaxValue)] int id)
        {
            var coin = await _coinExchangeRepository.GetCoinRateAllByIdAsync(id);

            return coin;
        }

        public async Task<CoinDto> GetCoinsAllFullInformationAsync([Range(1, int.MaxValue)] int id)
        {
            var coin = await _coinRepository.GetCoinsAllFullInformationAsync(id);

            return coin;
        }

        public async Task AddСoinСoinExchangesAsync(string name, [Range(1577836800000, long.MaxValue)] long ticks = 1577836800000)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Parameter cannot be null", nameof(name));
            }

            try
            {
                var data = new DateTime().AddTicks(new DateTime(1970, 1, 1).Ticks + ticks * 10000);
                var newCoins = await _сoinFromAPI.TakeCoinNameFromAPIAsync(name);
                var coinExchanges = await _сoinFromAPI.TakeCoinsFromAPIAsync(newCoins.Name, data);
                var answerCoins = await _coinRepository.AddAsync(newCoins);
                coinExchanges.ToList().ForEach(x => x.CoinId = answerCoins);
                await _coinExchangeRepository.AddCollectionAsync(coinExchanges);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Coin not found {name}", ex.Message);
            }
        }

        public async Task UpdateCoinsByCoinIdAsync([Range(1, int.MaxValue)] int id)
        {
            try
            {
                var lastDateTime = await _coinExchangeRepository.GetLastCoinRepositoryAsync(id);
                if( DateTime.Now.Ticks - lastDateTime.AddHours(8).Ticks < 0)
                {
                    throw new ArgumentException("8 hours have not yet passed for the update");
                }
                var coin = await _coinRepository.GetCoinsAllFullInformationAsync(id);
                var coinExchanges = await _сoinFromAPI.TakeCoinsFromAPIAsync(coin.Name, lastDateTime.AddHours(8));
                coinExchanges.ToList().ForEach(x => x.CoinId = id);
                await _coinExchangeRepository.AddCollectionAsync(coinExchanges);
            }
            catch (Exception)
            {
                await _coinRepository.DeleteAsync(id);
            }
        }

        public async Task DeleteCoinAsync([Range(1, int.MaxValue)] int id)
        {
            await _coinRepository.DeleteAsync(id);
        }
    
    }
}
