using MailGraphAnalysis.DTO;
using System.ComponentModel.DataAnnotations;

namespace MailGraphAnalysis.Entity.DB
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
