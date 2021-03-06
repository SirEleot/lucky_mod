using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string EmailConfirmCode { get; set; }
        public string Password { get; set; }
        public string Promocode { get; set; }
    }
}
