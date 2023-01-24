using AutoMapper;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Controller
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

        //[Authorize("ALLAdministrator")]
        [HttpPost("send-letter")]
        public async Task<IActionResult> SendLetter(LetterVM letter)
        {
            await _letterService.SendLetterAsync(letter.TextBody, letter.TextSubject, letter.UserEmail);

            return Ok(true);
        }

        ////[Authorize("ALLAdministrator")]
        //[HttpGet("get-history-lette/{id}")]
        //public async Task<IActionResult> StatusLetterByUserId([Range(1, int.MaxValue)] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var dispatches = await _letterService.StatusLetterByUserIdAsync(id);

        //    var result = _mapper.Map<List<LetterStatusVM>>(dispatches);

        //    return Ok(result);
        //}

        ////[Authorize("ALLAdministrator")]
        //[HttpGet("get-lette/{id}")]
        //public async Task<IActionResult> LetterByHistoryLetteId([Range(1, int.MaxValue)] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var dispatches = await _letterService.GetByIdAsync(id);

        //    var result = _mapper.Map<LetterVM>(dispatches);

        //    return Ok(result);
        //}
    }
}

