namespace MyBank.Domain.Entities
{
    public class User : BaseEntity
    {
        public int IdData { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
