using AutoMapper;
using Сoin.Contracts.Repo;
using Сoin.Contracts.Services;
//using Сoin.Entity;
using Сoin.DTO;
using System.ComponentModel.DataAnnotations;
using Сoin.Contracts.Persistence;
using Сoin.Data;

namespace Сoin.Logic
{
    public class CoinService : ICoinService
    {
        private readonly ICoinAPI _сoinFromAPI;
        private readonly ICoinRepository _coinRepository;
        private readonly ICoinExchangeRepository _coinExchangeRepository;
        private const long data2020 = 1546300800000;
        private const long data2021 = 1577836800000;
        private const long data1970 = 621355968000000000;

        public CoinService(ICoinRepository dispatchRepository, ICoinExchangeRepository coinExchangeRepository, ICoinAPI сoinFromAPI)
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

        public async Task<ICollection<CoinRateDto>> GetCoinRateAllByIdAsync([Range(1, int.MaxValue)] int id, [Range(24, int.MaxValue)] int step)
        {
            var coins = await _coinExchangeRepository.GetCoinRateAllByIdAsync(id, step);

            return coins;
        }

        public async Task<CoinDto> GetCoinsAllFullInformationAsync([Range(1, int.MaxValue)] int id)
        {
            var coin = await _coinRepository.GetCoinsAllFullInformationAsync(id);

            return coin;
        }

        public async Task AddСoinСoinExchangesAsync(string name, [Range(data2020, long.MaxValue)] long ticks = data2021)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Parameter cannot be null", nameof(name));
            }

            try
            {
                var data = new DateTime().AddTicks(data1970 + ticks * 10000);
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
                if (DateTime.Now.Ticks - lastDateTime.AddHours(8).Ticks < 0)
                {
                    throw new ArgumentException("8 hours have not yet passed for the update");
                }
                var coin = await _coinRepository.GetCoinsAllFullInformationAsync(id);
                var coinExchanges = await _сoinFromAPI.TakeCoinsFromAPIAsync(coin.Name, lastDateTime.AddHours(8));
                coinExchanges.ToList().ForEach(x => x.CoinId = id);
                await _coinExchangeRepository.AddCollectionAsync(coinExchanges);
            }
            catch (Exception er)
            {
                throw new ArgumentException(er.Message);
            }
        }

        public async Task DeleteCoinAsync([Range(1, int.MaxValue)] int id)
        {
            try
            {
                await _coinRepository.DeleteAsync(id);
            }
            catch (Exception er)
            {
                throw new ArgumentException(er.Message);
            }
        }

    }
}
