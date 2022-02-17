using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenService.Models;

namespace AuthorizationServer.Models
{
    public class UpdateTokenForm
    {
        /// <summary>
        /// Application key
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// Refresh token string base64 encoded
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
