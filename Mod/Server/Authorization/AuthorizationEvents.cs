using Server.Core.Entities;
using Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Shared.Authorization.Constants;
using GTANetworkAPI;
using Server.Database;

namespace Server.Authorization
{
    class AuthorizationEvents : Script
    {
        public int Priority => 10;
        public AuthorizationEvents()
        {
            Lucky.OnPlayerConnected += OnPlayerConnected;
            ServerDbContext.ModelCreating += AuthorizationService.ConfigureDbModels;
        }        

        [RemoteProc(AuthEventNames.ServerCheckToken)]
        public string CheckToken(LuckyPlayer player, string access_token)
        {
            return AuthorizationService.CheckAccessToken(player, access_token);
        }       

        private void OnPlayerConnected(LuckyPlayer player)
        {
            player.TriggerEvent(AuthEventNames.ClientBeginePlayerAuth, Lucky.LuckySettings.AppKey);
        }
    }
}
