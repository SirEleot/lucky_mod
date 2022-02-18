using Client.Utils;
using Newtonsoft.Json.Linq;
using RAGE;
using RAGE.Ui;
using Shared.Authorization.Constants;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Shared.Browsers.Actions.Authorization;

namespace Client.Authorization
{
    static class AuthorizationService
    {
        private static HtmlWindow _browser;
        private static readonly string _browserUrl = "package://auth/index.html";

        internal async static void BeginePlayerAuth(object[] args)
        {
            var appKey = args[0].ToString();
            if (_browser == null) _browser = new HtmlWindow(_browserUrl);
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

        internal static void ResetToken(object[] args)
        {
            Storage.Remove("auth_token");
        }

        internal static void WrongSocialId(object[] args)
        {
            _browser.Dispatch(new WronSocialAction());
        }

        private static void SetToken(string auth_token)
        {
            _browser.Dispatch(new SetTokenAction(auth_token));
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
            _browser.Dispatch(new SetAppKeyAction(appKey));
        }

        internal static void RpcCallback(object[] args)
        {
            _browser.ExecuteJs($"serverProcCallback({args[0]}, {JsonConvert.SerializeObject(args[1])})");
        }        
    }
}
