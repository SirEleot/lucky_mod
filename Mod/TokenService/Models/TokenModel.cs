using System;
using System.Collections.Generic;
using System.Text;

namespace TokenService.Models
{
    public class TokenModel
    {
        public TokenModel()
        {

        }

        public TokenModel(string login, string email, string appKey)
        {
            Login = login;
            Email = email;
            AppKey = appKey;
            Expiried = DateTime.UtcNow.AddMinutes(TokenService.AccessTokenLifeTimeInMinutes);
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string AppKey { get; set; }
        public DateTime Expiried { get; set; }
        public string Sign { get; set; }

        public bool IsAccessToken()
        {
            return Id == 0;
        }
        public string[] ParametersToArray(string privateKey)
        {
            return new string[] { Id.ToString(), Login, Email, privateKey, AppKey, Expiried.ToString() };
        }
    }
}
