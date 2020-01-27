using MyBank.Domain.Entities;

namespace MyBank.Domain.Users.Entities
{
    public class User : BaseEntity
    {
        public int IdData { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
