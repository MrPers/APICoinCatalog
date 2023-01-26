using AutoMapper;
using MailGraphAnalysis.Models;
using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;
using System;

namespace MailGraphAnalysis
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CoinRateDto, CoinRateVM>()
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => GetJavascriptTimestamp(src.Time)));
            CreateMap<CoinRate, CoinRateDto>().ReverseMap();
            CreateMap<CoinRateJSON, CoinRate>()
                .ForMember(dst => dst.Prices, opt => opt.MapFrom(src => (src.PriceHigh + src.PriceLow)/2));

            CreateMap<CoinJSON, Coin>()
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description.En))
                .ForMember(dst => dst.URLImage, opt => opt.MapFrom(src => src.URLImage.Thumb));
            CreateMap<Coin, CoinDto>().ReverseMap();
            CreateMap<CoinDto, CoinVM>();
            CreateMap<CoinDto, CoinFullVM>();

            CreateMap<LetterVM, LetterDto>().ReverseMap();
            CreateMap<Letter, LetterDto>().ReverseMap();
        }

        static long GetJavascriptTimestamp(DateTime input)
        {
            var span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            var time = input.Subtract(span);
            return (long)(time.Ticks / 10000);
        }
    }
}
