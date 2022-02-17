using GTANetworkAPI;
using MessagePack;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Core.Entities;
using Server.Core.Models;
using Server.Database;
using Shared.Authorization.Constants;
using Shared.Storage.Constants;
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
    class AuthorizationService
    {
        private static TokenService.TokenService _tokenSrvice;
        public AuthorizationService()
        {
            _tokenSrvice = TokenServiceManager.GetInstance(Lucky.LuckySettings.PrivateKey);
        }
        internal static string CheckAccessToken(LuckyPlayer player, string access_token)
        {
            var token = _tokenSrvice.DeserealizeToken(access_token);
            if (token == null || !_tokenSrvice.IsValidTokenSign(token))
                return "error";    
            player.AccessToken = token;
            LoadPlayerAccount(player, player.SocialClubId, player.AccessToken);
            return "ok";     
        }
       
        private async static void LoadPlayerAccount(LuckyPlayer player, ulong socialClubId, TokenModel token)
        {
            var account = await DbManager.Cache.Accounts
                   .Include(a => a.Characters)
                   .FirstOrDefaultAsync(p => p.Login == player.AccessToken.Login);
            if (account == null)
            {
                account = new LuckyAccount(socialClubId, token.Login, token.Email);
                await DbManager.AddAsync(account);
            }
            else
            {
                await NAPI.Task.WaitForMainThread();
                if (account.SocialId != socialClubId)
                    player.TriggerEvent(AuthEventNames.ClientAuthSocialWrong);
                else
                    player.TriggerEvent(AuthEventNames.ClientAuthCharacterSelect, player.Account.GetCharacterListData());
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
