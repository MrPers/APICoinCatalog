using AutoMapper;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.Models;
using MailGraphAnalysis.DB.Models;
using MailGraphAnalysis.DTO;
using MailGraphAnalysis.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("get-all-coins")]
        public async Task<IActionResult> GetCoinsAll()
        {
            var coins = await _coinService.GetAllAsync();
            var coinsResult = _mapper.Map<List<CoinVM>>(coins);

            IActionResult result = coinsResult == null ? NotFound() : Ok(coinsResult);

            return result;
        }

        [HttpGet("get-all-coins-with-previous-information")]
        public async Task<IActionResult> GetCoinsAllWithPreviousInformation()
        {
            var coins = await _coinService.GetCoinsAllWithPreviousInformation();
            var coinsResult = _mapper.Map<List<CoinsWithPreviousInformationVM>>(coins);

            IActionResult result = coinsResult == null ? NotFound() : Ok(coinsResult);

            return result;
        }

        [HttpGet("get-by-coin-id-coinExchanges/{id}")]
        public async Task<IActionResult> GetCoinById(int id)
        {
            var coins = await _coinService.GetByIdAsync(id);
            var commentsResult = _mapper.Map<List<CoinExchangeVM>>(coins);
            IActionResult result = coins == null ? NotFound() : Ok(commentsResult);

            return result;
        }

        [HttpPost("add-coin-&-coinExchanges")]
        public async Task<IActionResult> AddAsync(string name)
        {
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

        [HttpPut("update-by-coin-id-coinExchanges")]
        public async Task<IActionResult> UpdateByCoinAsync(int Id)
        {
            try
            {
                await _coinService.UpdateByCoinIdAsync(Id);

                return Ok(true);
            }
            catch (Exception)
            {
                return NoContent();
            }

        }

        [HttpDelete("delete-coin-and-coinExchanges/{id}")]
        public async Task<IActionResult> DeleteCoinAndCoinExchanges(int id)
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
