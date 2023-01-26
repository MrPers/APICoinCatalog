using AutoMapper;
using MailGraphAnalysis.Contracts.Persistence;
using MailGraphAnalysis.DTO;
using MailGraphAnalysis.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Data.Persistence
{
    public class CoinFromAPI: ICoinFromAPI
    {
        protected readonly IOptions<MySettingsModelDto> _appSettings;
        protected readonly IMapper _mapper;

        public CoinFromAPI(
            IOptions<MySettingsModelDto> appSettings, IMapper mapper
        ){
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public async Task<IList<Coin>> TakeCoinsNameFromAPIAsync(ICollection<string> names) 
        {
            var client = new HttpClient();
            List<Coin> coins = new();

            foreach (var name in names)
            {
                HttpResponseMessage response = await client.GetAsync(
                $"https://api.coingecko.com/api/v3/coins/{name}?tickers=false&market_data=false&community_data=false&developer_data=false&sparkline=false");
                var result = await response.Content.ReadAsStringAsync();

                var rootObjects = JsonConvert.DeserializeObject<CoinJSON>(result);
                coins.Add(_mapper.Map<Coin>(rootObjects));
            }

            client.Dispose();

            return coins;
        }

        public async Task<IList<CoinRate>> TakeCoinsFromAPIAsync(ICollection<CoinDto> names)
        {
            string dataStart = "2020-01-01T00:00:00";
            List<CoinRate> coins = new();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_appSettings.Value.Url);
            client.DefaultRequestHeaders.Add("X-CoinAPI-Key", _appSettings.Value.Key);
            var timeCoins = _mapper.Map<List<Coin>>(names);

            foreach (var name in timeCoins)
            {
                //var dataStart = DateTime.Now.AddHours(-(limitHours + 1)).ToString("yyyy-MM-ddTHH:mm:ss");
                HttpResponseMessage response = await client
                    .GetAsync($"v1/ohlcv/BITSTAMP_SPOT_{name.Name}_USD/history?period_id=8HRS&time_start={dataStart}&limit={_appSettings.Value.LimitHours}");

                var result = await response.Content.ReadAsStringAsync();
                var rootObjects = JsonConvert.DeserializeObject<List<CoinRateJSON>>(result);
                var t = _mapper.Map<List<CoinRate>>(rootObjects);
                coins?.AddRange(t);
                //coins.ToList().ForEach(n => n.Id = 2);
            }

            client.Dispose();

            return coins;
        }

    }
}
