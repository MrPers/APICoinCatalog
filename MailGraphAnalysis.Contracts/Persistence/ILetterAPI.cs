using MailGraphAnalysis.DTO;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Persistence
{
    public interface ILetterAPI
    {
        Task SendLetterAsync(ICollection<LetterDto> letters);
    }
}
