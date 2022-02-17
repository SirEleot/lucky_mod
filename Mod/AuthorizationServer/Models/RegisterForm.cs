using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class RegisterForm
    {
        /// <summary>
        /// Login string
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Email string
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password string
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Confirm password string
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Promocode string
        /// </summary>
        public string Promocode { get; set; }
        /// <summary>
        /// Application key
        /// </summary>
        public string AppKey { get; set; }
    }
}
