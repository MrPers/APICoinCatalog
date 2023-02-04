using MailGraphAnalysis.Contracts.Persistence;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Contracts.Services;
using MailGraphAnalysis.DTO;
using MailKit.Net.Smtp;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Logic.Service
{
    public class LetterService : ILetterService
    {
        private ILetterRepository _letterRepository;
        private ILetterAPI _letterBusiness;

        public LetterService(
            ILetterRepository dispatchRepository,
            ILetterAPI letterBusiness
        )
        {
            _letterRepository = dispatchRepository;
            _letterBusiness = letterBusiness;
        }

        public async Task SendLetterAsync(ICollection<LetterDto> letters)
        {
            if (letters == null)
            {
                throw new ArgumentNullException(nameof(letters));
            }

            try
            {
                //await _letterBusiness.SendLetterAsync(letters);

                //string Emails = "";

                //foreach (var userEmail in usersEmail)
                //{
                //    Emails = Emails + "|" + userEmail;
                //}

                //await _letterRepository.AddAsync(new LetterDto()
                //{
                //    TimeSend = DateTime.Now,
                //    TextBody = textBody,
                //    TextSubject = textSubject,
                //    UserEmail = Emails // no time to change
                //});
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(letters));
            }
        }

    }
}