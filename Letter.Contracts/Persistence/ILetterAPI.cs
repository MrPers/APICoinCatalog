using Letter.DTO;

namespace Letter.Contracts.Persistence
{
    public interface ILetterAPI
    {
        Task SendLetterAsync(ICollection<LetterDto> letters);
    }
}
