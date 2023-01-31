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
            try
            {
                var coins = await _coinService.GetCoinsAllPreviousInformationAsync();
                var coinsResult = _mapper.Map<List<CoinVM>>(coins);

                return Ok(coinsResult);
            }
            catch (Exception)
            {
                return NotFound();
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
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("get-coinExchanges-by-coin-id/{id}")]
        public async Task<IActionResult> GetCoinById([Range(1, int.MaxValue)] int id)
        {
            try
            {
                var coins = await _coinService.GetCoinRateAllByIdAsync(id);
                var commentsResult = _mapper.Map<List<CoinRateVM>>(coins);

                return Ok(commentsResult);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        //так можно
        [HttpPost("add-coin-&-coinExchanges/{name}")]
        public async Task<IActionResult> AddСoinСoinExchangesAsync([FromRoute] string name, [Range(1546300800000, long.MaxValue)] long ticks = 1577836800000)
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
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-coin-by-id-coin")]
        public async Task<IActionResult> UpdateCoinAsync([Range(1, int.MaxValue)] int id)
        {
            try
            {
                await _coinService.UpdateCoinsByCoinIdAsync(id);

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
                await _coinService.DeleteCoinAsync(id);

                return Ok(true);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
    }
}
