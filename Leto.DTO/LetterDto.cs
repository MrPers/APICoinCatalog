using Base.DTO;
using Base.Entity;

namespace Letter.DTO
{
    public class LetterDto : BaseDto<int>
    {
        public DateTime TimeSend { get; set; }
        public string TextSubject { get; set; }
        public string TextBody { get; set; }
        public string UserEmail { get; set; }
    }

}
