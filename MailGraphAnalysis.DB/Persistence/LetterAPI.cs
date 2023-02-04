using MailGraphAnalysis.Contracts.Persistence;
using MailGraphAnalysis.DTO;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MailGraphAnalysis.Services
{
    public class LetterAPI : ILetterAPI
    {
        private IOptions<MySettingsModelDto> _appSettings;

        public LetterAPI(
            IOptions<MySettingsModelDto> appSettings
        )
        {
            _appSettings = appSettings;
        }

        public async Task SendLetterAsync(ICollection<LetterDto> letters)
        {
            //MimeMessage emailMessage = CreateLetter(textBody, textSubject, usersEmail);

            //await SendLetterAsync(emailMessage);
        }

        private MimeMessage CreateLetter(string textBody, string textSubject, ICollection<string> usersEmail)
        {
            var emailMessage = new MimeMessage();
            var builder = new BodyBuilder();

            emailMessage.From.Add(new MailboxAddress(_appSettings.Value.Name, _appSettings.Value.Address));
            foreach (var userEmail in usersEmail)
            {
                emailMessage.To.Add(new MailboxAddress("", userEmail));
            }
            emailMessage.Subject = textSubject;

            // Set the plain-text version of the message text
            builder.TextBody = textBody;
            // We may also want to attach a calendar event for Monica's party...
            builder.Attachments.Add(@"C:\Users\Anton\Desktop\RT.txt");

            // Now we just need to set the message body and we're done
            emailMessage.Body = builder.ToMessageBody();

            return emailMessage;
        }

        private async Task SendLetterAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_appSettings.Value.SmtpClient, _appSettings.Value.Port, false);
                await client.AuthenticateAsync(_appSettings.Value.Address, _appSettings.Value.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);

                client.Dispose();
            }
        }

    }
}
