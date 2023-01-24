using System;
using System.Collections.Generic;

namespace MailGraphAnalysis.DTO
{
    public class LetterDto : IBaseEntity<int>
    {
        public int Id { get; set; }
        public DateTime TimeSend { get; set; }
        public string TextSubject { get; set; }
        public string TextBody { get; set; }
        public string UserEmail { get; set; }
    }
}
