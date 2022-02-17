using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class LoginForm
    {
        /// <summary>
        /// Login string
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Password string
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Application key
        /// </summary>
        public string AppKey { get; set; }
    }
}
