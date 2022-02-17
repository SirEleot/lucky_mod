using System;
using System.Collections.Generic;
using System.Text;

namespace TokenService.Models
{
    public class TokensPairModel
    {
        public TokenModel AccessToken { get; set; }
        public TokenModel RefreshToken { get; set; }
    }
}
