using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Domain.Entities.User
{
    public class Auth
    {
        public string Agency { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
