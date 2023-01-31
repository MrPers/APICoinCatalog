using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailGraphAnalysis.Entity.DB
{
    public class Letter : BaseEntity<int>
    {
        public DateTime TimeSend { get; set; }
        public string TextSubject { get; set; }
        public string TextBody { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string UserEmail { get; set; }
    }
}
