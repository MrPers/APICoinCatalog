using MailGraphAnalysis.DTO;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysisGraphAnalysis.DB.Models
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
