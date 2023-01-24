using MailGraphAnalysis.DTO;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailGraphAnalysis.Contracts.Business
{
    public interface ILetterBusiness
    {
        Task SendLetterAsync(string textBody, string textSubject, ICollection<string> usersEmail);
        private MimeMessage CreateLetter(string textBody, string textSubject, ICollection<string> usersEmail)
        {
            var emailMessage = new MimeMessage();

            return emailMessage;
        }
        private Task SendLetterAsync(MimeMessage emailMessage)
        {
            return Task.FromResult(true);
        }
    }
}
