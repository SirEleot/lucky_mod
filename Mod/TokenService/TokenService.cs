using System;
using System.Security.Cryptography;
using System.Text;
using TokenService.Models;
using System.Text.Json;

namespace TokenService
{
    public class TokenService
    {
        private readonly string _privateKey;
        internal static readonly int AccessTokenLifeTimeInMinutes = 720;
        private static readonly string _signSeparator = "@:";
        public TokenService(string privateKey)
        {
            _privateKey = privateKey;
        }

        public TokensPairModel CreateTokensPair(string login, string email, string appKey)
        {
            var accessToken = new TokenModel(login, email, appKey);
            var refreshToken = new TokenModel(login, email, appKey);
            return new TokensPairModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public void SignTokens(TokensPairModel tokensPair)
        {
            if (tokensPair.RefreshToken.IsAccessToken()) throw new Exception("Refresh token should have Id");
            SignToken(tokensPair.RefreshToken);
            SignToken(tokensPair.AccessToken);
        }
        public bool IsValidTokenSign(TokenModel token)
        {
            return token?.Sign != null && token.Sign == CreateSign(token) && DateTime.ParseExact(token.Expiried, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) > DateTime.UtcNow;
        }

        public string SerealizeTokens(TokensPairModel tokensPair)
        {
            return Base64Encode(JsonSerializer.Serialize(tokensPair));
        }
        public TokenModel DeserealizeToken(string token)
        {
            try
            {
                return JsonSerializer.Deserialize<TokenModel>(Base64Decode(token));
            }
            catch (Exception)
            {
                return null;
            }            
        }

        private void SignToken(TokenModel token)
        {
            token.Sign = CreateSign(token);
        }
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private string CreateSign(TokenModel token)
        {
            var args = token.ParametersToArray(_privateKey);
            var argString = String.Join(_signSeparator, args);
            Console.WriteLine($"|{argString}|");
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(argString));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                Console.WriteLine(builder.ToString());
                return builder.ToString();
            }
        }
    }
}
