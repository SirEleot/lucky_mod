using Server.Core.Entities;
using Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Server.Database;
using Shared.Events;

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

        [RemoteProc(AuthorizationEventNames.ServerProcCheckToken)]
        public string CheckToken(LuckyPlayer player, string access_token)
        {
            return AuthorizationService.CheckAccessToken(player, access_token);
        }       

        private void OnPlayerConnected(LuckyPlayer player)
        {
            player.TriggerEvent(AuthorizationEventNames.ClientBeginePlayerAuth, Lucky.LuckySettings.AppKey);
        }
    }
}
