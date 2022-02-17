using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Authorization
{
    public class AuthToken
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime Expiried { get; set; }
        [NotMapped]
        public string Sign { get; set; }
    }
}
