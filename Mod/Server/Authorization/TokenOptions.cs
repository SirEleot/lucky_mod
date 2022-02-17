using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Authorization
{
    public class TokenOptions
    {
        public const string KEY = "mysupersecret_secretkey!123";
        public const string ISSUER = "AuthorizationServer"; // издатель токена
        public const string APPKEY = "111111111";
        public const string APIURI = "https://localhost:44327/Auth/Update";
    }
}
