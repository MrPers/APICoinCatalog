﻿using MailGraphAnalysis.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Services
{
    public interface ILetterService
    {
        Task SendLetterAsync(ICollection<LetterDto> letters);
    }
}
