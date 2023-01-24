namespace MailGraphAnalysis.DTO
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
