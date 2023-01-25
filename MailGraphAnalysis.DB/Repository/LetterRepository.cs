using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.Data;
using MailGraphAnalysis.Entity;
using MailGraphAnalysis.DTO;

namespace MailGraphAnalysis.Data.Repository
{
    public class LetterRepository : BaseRepository<Letter, LetterDto, int>, ILetterRepository
    {
        public LetterRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}