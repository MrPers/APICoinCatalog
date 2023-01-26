using AutoMapper;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.Models;
using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;
using MailGraphAnalysis.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly ICoinService _coinService;
        private readonly IMapper _mapper;

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
            var coins = await _coinService.GetCoinsAllPreviousInformationAsync();
            var coinsResult = _mapper.Map<List<CoinVM>>(coins);

            IActionResult result = coinsResult == null ? NotFound() : Ok(coinsResult);

            return result;
        }

        [HttpGet("get-coin-full-information-by-coin-id/{id}")]
        public async Task<IActionResult> GetCoinFullInformation([Range(1, int.MaxValue)] int id)
        {
            var coins = await _coinService.GetCoinsAllFullInformationAsync();
            var coinsResult = _mapper.Map<List<CoinVM>>(coins);

            IActionResult result = coinsResult == null ? NotFound() : Ok(coinsResult);

            return result;
        }

        [HttpGet("get-coinExchanges-by-coin-id/{id}")]
        public async Task<IActionResult> GetCoinById([Range(1, int.MaxValue)] int id)
        {
            var coins = await _coinService.GetByIdAsync(id);
            var commentsResult = _mapper.Map<List<CoinRateVM>>(coins);
            IActionResult result = coins == null ? NotFound() : Ok(commentsResult);

            return result;
        }

        [HttpPost("add-coin-&-coinExchanges")]
        public async Task<IActionResult> AddAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return ValidationProblem();
            }

            try
            {
                var coin = new CoinDto() { Name = name };
                await _coinService.AddAsync(coin);

                return Ok(true);
            }
            catch (Exception)
            {
                return ValidationProblem();
            }
        }

        [HttpPut("update-coin-by-id-coinExchanges")]
        public async Task<IActionResult> UpdateByCoinAsync([Range(1, int.MaxValue)] int id)
        {
            try
            {
                await _coinService.UpdateByCoinIdAsync(id);

                return Ok(true);
            }
            catch (Exception)
            {
                return NoContent();
            }

        }

        [HttpDelete("delete-coin-and-coinExchanges/{id}")]
        public async Task<IActionResult> DeleteCoinAndCoinRate([Range(1, int.MaxValue)]int id)
        {
            try
            {
                await _coinService.DeleteAsync(id);

                return Ok(true);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
    }
}
