using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiApplication.Models
{    public class Account
    {
        public Guid Id { get; set; }      
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role[] Roles { get; set; }
    }
}
