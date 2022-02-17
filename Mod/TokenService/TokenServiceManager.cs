using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TokenService
{
    public static class TokenServiceManager
    {
        private static TokenService _instance;
        public static void AddTokenService(this IServiceCollection services, string privateKey)
        {
            services.AddSingleton(GetInstance(privateKey));
        }
        public static TokenService GetInstance(string privateKey)
        {
            if (_instance == null) _instance = new TokenService(privateKey);
            return _instance;
        }
    }
}
