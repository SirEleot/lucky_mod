using Client.Utils;
using Newtonsoft.Json.Linq;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Shared.Browsers.Actions.Authorization;
using Shared.DTO;
using Client.Gui;
using Shared.Events;

namespace Client.Authorization
{
    static class AuthorizationService
    {
        internal async static void BeginePlayerAuth(object[] args)
        {
            var appKey = args[0].ToString();
            SetAppKey(appKey);
            var auth_token = await Storage.Get("auth_token");
            SetToken(auth_token);
            Cursor.Visible = true;
        }

        internal static void UpdateToken(object[] args)
        {
            var token = args[0].ToString();
            Storage.Set("auth_token", token);
        }

        internal static void ToCharacterSelect(object[] args)
        {
            var serealizedCharactersData = (byte[])args[0];
            var charactersDataList = RAGE.Util.MsgPack.Deserialize<List<LuckyCharacterDTO>>(serealizedCharactersData);
            RAGE.Ui.Console.Log(ConsoleVerbosity.Info, JsonConvert.SerializeObject(charactersDataList));

        }

        internal static void DoCharacterCreate(object[] args)
        {
            Lucky.IsLoading = true;
            var id = Convert.ToInt32(args[0]);
            Events.CallRemote(AuthorizationEventNames.ServerRequestCharaterCreate, id);
        }

        internal static void DoCharacterSelect(object[] args)
        {
            Lucky.IsLoading = true;
            var id = Convert.ToInt32(args[0]);
            Events.CallRemote(AuthorizationEventNames.ServerCharaterSelect, id);
        }

        internal static void ResetToken(object[] args)
        {
            Storage.Remove("auth_token");
        }

        internal static void WrongSocialId(object[] args)
        {
            BrowserManager.Dispatch(new WrongSocialAction());
        }

        private static void SetToken(string auth_token)
        {
            BrowserManager.Dispatch(new SetTokenAction(auth_token));
        }
       
        //internal static void AuthComplite(object[] args)
        //{
        //    var access_token = args[0].ToString();
        //    var refresh_token = args[0].ToString();
        //    Storage.Set("access_token", access_token);
        //    Storage.Set("refresh_token", refresh_token);
        //    Events.CallRemote(AuthEventNames.ServerPlayerLoggined, access_token);
        //    Cursor.Visible = false;
        //}
       
        internal static void SetAppKey(string appKey)
        {
            BrowserManager.Dispatch(new SetAppKeyAction(appKey));
        }

        internal static void RpcCallback(object[] args)
        {
            BrowserManager.ProcCallbackAuth(args[0].ToString(), JsonConvert.SerializeObject(args[1]));
        }        
    }
}
