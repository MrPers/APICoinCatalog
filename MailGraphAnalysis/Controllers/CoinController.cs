using AutoMapper;
using Сoin.Contracts.Services;
using Сoin.Api.Models;
using Сoin.Entity;
using Сoin.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Сoin.Api.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;
        private readonly IMapper _mapper;
        private const long data2020 = 1546300800000;
        private const long data2021 = 1577836800000;

        public CoinController(
            ICoinService coinService,
            IMapper mapper
        )
        {
            _coinService = coinService;
            _mapper = mapper;
        }

        [HttpGet("get-coins-all-previous-information")]
        public async Task<IActionResult> GetCoinsAllPreviousInformation()
        {
            try
            {
                var coins = await _coinService.GetCoinsAllPreviousInformationAsync();
                var coinsResult = _mapper.Map<List<CoinVM>>(coins);

                return Ok(coinsResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-coin-full-information-by-coin-id/{id}")]
        public async Task<IActionResult> GetCoinFullInformation([Range(1, int.MaxValue)] int id)
        {
            try
            {
                var coins = await _coinService.GetCoinsAllFullInformationAsync(id);
                var coinsResult = _mapper.Map<CoinFullVM>(coins);

                return Ok(coinsResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get-coinExchanges")]
        public async Task<IActionResult> GetCoinsById(CoinRateQuestion coinRateQuestion)
        {
            if (coinRateQuestion == null || coinRateQuestion.Id < 1 || coinRateQuestion.Step < 24)
            {
                throw new ArgumentNullException(nameof(coinRateQuestion));
            }

            try
            {
                var coins = await _coinService.GetCoinRateAllByIdAsync(coinRateQuestion.Id, coinRateQuestion.Step);
                
                return Ok(coinRateQuestion.InTick ? _mapper.Map<List<CoinRateVMInTicks>>(coins) : _mapper.Map<List<CoinRateVMInDateTime>>(coins));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-coin-&-coinExchanges/{name}")]
        public async Task<IActionResult> AddСoinСoinExchangesAsync([FromRoute] string name, [Range(data2020, long.MaxValue)] long ticks = data2021)
        {
            if (string.IsNullOrEmpty(name))
            {
                return ValidationProblem();
            }

            try
            {
                await _coinService.AddСoinСoinExchangesAsync(name, ticks);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("update-coin-by-id-coin/{id}")]
        public async Task<IActionResult> UpdateCoinAsync([Range(1, int.MaxValue)] int id)
        {
            try
            {
                await _coinService.UpdateCoinsByCoinIdAsync(id);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-coin-and-coinExchanges/{id}")]
        public async Task<IActionResult> DeleteCoinAndCoinRate([Range(1, int.MaxValue)]int id)
        {
            try
            {
                await _coinService.DeleteCoinAsync(id);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
