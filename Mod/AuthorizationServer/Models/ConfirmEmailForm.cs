using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Models
{
    public class ConfirmEmailForm
    {
        /// <summary>
        /// Email string
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Conformation code froam email
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Application key
        /// </summary>
        public string AppKey { get; set; }
    }
}
