//using MailGraphAnalysis.DB.Models;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MailGraphAnalysis.Business
//{
//    public static class CoinFromAPI
//    {
//        private const string key = "A28CDFC3-9421-46D0-B585-C3F2CA053B18";
//        private const uint limitHours = 9200;
//        private const string Url = "https://rest.coinapi.io/";

//        public static async Task<IList<CoinExchange>> TakeCoinsFromAPIAsync(ICollection<Coin> names)
//        {
//            List<CoinExchange> coins = new();
//            var client = new HttpClient();
//            client.BaseAddress = new Uri(Url);
//            client.DefaultRequestHeaders.Add("X-CoinAPI-Key", key);

//            foreach (var name in names)
//            {
//                //var dataStart = DateTime.Now.AddHours(-(limitHours + 1)).ToString("yyyy-MM-ddTHH:mm:ss");
//                string dataStart = "2020-01-01T00:00:00";
//                HttpResponseMessage response = await client
//                    .GetAsync($"v1/ohlcv/BITSTAMP_SPOT_{name.Name}_USD/history?period_id=8HRS&time_start={dataStart}&limit={limitHours}");

//                if (response.IsSuccessStatusCode)
//                {
//                    coins.AddRange(await ReadCoinExchangesAsync(name, response));
//                }
//                else
//                {
//                    throw new Exception("Success Status Fals");
//                }
//            }

//            client.Dispose();

//            return coins;
//        }

//        private static async Task<List<CoinExchange>> ReadCoinExchangesAsync(Coin name, HttpResponseMessage response)
//        {
//            List<CoinExchange> coins = new List<CoinExchange>();
//            var result = await response.Content.ReadAsStringAsync();
//            var rootObjects = JsonConvert.DeserializeObject<List<CoinJson>>(result);

//            foreach (var rootObject in rootObjects)
//            {
//                //DateTime rounded = rootObject.Time.AddMilliseconds(-rootObject.Time.Millisecond).AddSeconds(-rootObject.Time.Second);
//                //int minutes = rounded.Minute < 25 ? 0 : rounded.Minute > 55 ? 60 : 30;
//                //rounded = rounded.AddMinutes(minutes - rounded.Minute);

//                //coins.Add(new CoinExchange((float)(rootObject.PriceHigh + rootObject.PriceLow) / 2, (float)rootObject.VolumeTraded, rounded, name.Id));
//                coins.Add(new CoinExchange((float)(rootObject.PriceHigh + rootObject.PriceLow) / 2, (float)rootObject.VolumeTraded, rootObject.Time, name.Id));
//            }
//            return coins;
//        }
//    }
//}
