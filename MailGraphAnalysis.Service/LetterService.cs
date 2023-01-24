using MailGraphAnalysis.Contracts.Business;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.DTO;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Services
{
    public class LetterService : ILetterService
    {
        private ILetterRepository _letterRepository;
        private ILetterBusiness _letterBusiness;

        public LetterService(
            ILetterRepository dispatchRepository,
            ILetterBusiness letterBusiness
        )
        {
            _letterRepository = dispatchRepository;
            _letterBusiness = letterBusiness;
        }

        public async Task SendLetterAsync(string textBody, string textSubject, ICollection<string> usersEmail)
        {
            //try
            //{
                await _letterBusiness.SendLetterAsync(textBody, textSubject, usersEmail);

                string Emails = "";

                foreach (var userEmail in usersEmail)
                {
                    Emails = Emails + "|" + userEmail;
                }

                await _letterRepository.AddAsync(new LetterDto()
                {
                    TimeSend = DateTime.Now,
                    TextBody = textBody,
                    TextSubject = textSubject,
                    UserEmail = Emails // no time to change
                });
            //}
            //catch(Exception ex)
            //{
            //    throw new ArgumentNullException(nameof(product));
            //}
        }

    }
}