using AutoMapper;
using MailGraphAnalysis.Contracts.Repo;
using MailGraphAnalysis.DB;
using MailGraphAnalysis.DB.Models;
using MailGraphAnalysis.DTO;
using MailGraphAnalysis.Repository;

namespace MailGraphAnalysis.Repository
{
    public class LetterRepository : BaseRepository<Letter, LetterDto, int>, ILetterRepository
    {
        public LetterRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}