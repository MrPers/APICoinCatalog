using AutoMapper;
using Letter.Contracts.Services;
using Letter.DTO;
using Letter.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Letter.Api.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly ILetterService _letterService;
        private readonly IMapper _mapper;

        public LetterController(
            ILetterService letterService,
            IMapper mapper
        )
        {
            _letterService = letterService;
            _mapper = mapper;
        }

        [HttpPost("send-email-addresses")]
        public async Task<IActionResult> SendLetter(LetterVM letter)
        {
            if (letter == null || letter.UserEmail == null || letter.TextSubject == null || letter.TextBody == null)
            {
                throw new ArgumentNullException(nameof(letter));
            }

            try
            {
                var lettersDto = _mapper.Map<LetterDto>(letter);

                await _letterService.SendLetterAsync(lettersDto);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}