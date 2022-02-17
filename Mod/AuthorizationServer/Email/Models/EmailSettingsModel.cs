using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Email.Models
{
    public class EmailSettingsModel
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
