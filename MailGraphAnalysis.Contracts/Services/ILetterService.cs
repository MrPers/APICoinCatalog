using MailGraphAnalysis.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Services
{
    public interface ILetterService
    {
        Task SendLetterAsync(string textBody, string textSubject, ICollection<string> UserEmail);
    }
}
