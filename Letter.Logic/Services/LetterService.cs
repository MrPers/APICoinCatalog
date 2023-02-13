using Letter.Contracts.Business;
using Letter.Contracts.Persistence;
using Letter.Contracts.Repo;
using Letter.Contracts.Services;
using Letter.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace Letter.Logic.Services
{
    public class LetterService : ILetterService
    {
        private ILetterRepository _letterRepository;
        private ILetterAPI _letterAPI;
        private ILetterBusiness _letterBusiness; 

        public LetterService(
            ILetterRepository dispatchRepository,
            ILetterAPI letterAPI,
            ILetterBusiness letterBusiness
        )
        {
            _letterRepository = dispatchRepository;
            _letterAPI = letterAPI;
            _letterBusiness = letterBusiness;
        }

        public async Task SendLetterAsync(LetterDto letters)
        {
            if (letters == null)
            {
                throw new ArgumentNullException(nameof(letters));
            }

            try
            {
                var coins = await _letterAPI.GetCoinsRateDtoById(letters.IdCoin, letters.StepCoin);

                string filePath = _letterBusiness.MakingFile(coins);

                await _letterAPI.SendLetterAsync(letters, filePath);

                await _letterRepository.AddAsync(new LetterDto()
                {
                    TimeSend = DateTime.Now,
                    TextBody = letters.TextBody,
                    TextSubject = letters.TextSubject,
                    UserEmail = letters.UserEmail
                });
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(nameof(letters));
            }
        }

    }
}