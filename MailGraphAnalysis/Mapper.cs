using AutoMapper;
using MailGraphAnalysis.Models;
using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;

namespace MailGraphAnalysis
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CoinExchangeDto, CoinExchangeVM>()
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time.Ticks));
            //CreateMap<CoinExchangeVM, CoinExchangeDto>().ReverseMap();
            CreateMap<CoinExchange, CoinExchangeDto>().ReverseMap();
            CreateMap<CoinsWithPreviousInformationVM, CoinsWithPreviousInformationDto>().ReverseMap();
            CreateMap<CoinVM, CoinDto>().ReverseMap();
            CreateMap<Coin, CoinDto>().ReverseMap();
            CreateMap<LetterVM, LetterDto>().ReverseMap();
            CreateMap<Letter, LetterDto>().ReverseMap();
        }
    }
}
