using GTANetworkAPI;
using MessagePack;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Core.Entities;
using Server.Core.Models;
using Server.Database;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using TokenService;
using TokenService.Models;

namespace Server.Authorization
{
    static class AuthorizationService
    {
        private static TokenService.TokenService _tokenSrvice;
        static AuthorizationService()
        {
            _tokenSrvice = TokenServiceManager.GetInstance(Lucky.LuckySettings.PrivateKey);
        }
        internal static string CheckAccessToken(LuckyPlayer player, string access_token)
        {
            try
            {
                var token = _tokenSrvice.DeserealizeToken(access_token);
                if (token == null || !_tokenSrvice.IsValidTokenSign(token))
                    return "error";
                player.AccessToken = token;
                NAPI.Task.Run(()=> { LoadPlayerAccount(player, player.SocialClubId, player.AccessToken); }, 100);
                return "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "error";
            }
            
        }
       
        private async static void LoadPlayerAccount(LuckyPlayer player, ulong socialClubId, TokenModel token)
        {
            var account = await DbManager.Cache.Accounts
                   .Include(a => a.Characters)
                   .FirstOrDefaultAsync(p => p.Login == token.Login);
            if (account == null)
            {
                account = new LuckyAccount(socialClubId, token.Login, token.Email);
                await DbManager.AddAsync(account);
            }
            else
            {
                await NAPI.Task.WaitForMainThread();
                
                if (account.SocialId != socialClubId)
                    player.TriggerEvent(AuthorizationEventNames.ClientAuthSocialWrong);
                else
                {
                    player.Account = account;
                    player.TriggerEvent(AuthorizationEventNames.ClientAuthToCharacterSelect, account.GetCharacterListData());
                }
                    
            }
        }
       

        public static void ConfigureDbModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LuckyAccount>()
                .Property(a => a.SocialId)
                .HasConversion(s => s.ToString(), s => Convert.ToUInt64(s));
        }
    }
}
