using AutoMapper;
using Letter.DTO;
using Letter.Entity;

namespace Letter
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<LetterEntity, LetterDto>().ReverseMap();
        }
    }

}
