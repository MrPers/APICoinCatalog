using MailGraphAnalysis.DTO;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Persistence
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
