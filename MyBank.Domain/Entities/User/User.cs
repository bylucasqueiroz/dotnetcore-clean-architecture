using MyBank.Domain.Entities;

namespace MyBank.Domain.Users.Entities
{
    public class User : BaseEntity
    {
        public string Agency { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Office { get; set; }
    }
}
