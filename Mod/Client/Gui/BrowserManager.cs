using Client.Gui.Models;
using RAGE.Ui;
using Shared.Browsers;
using Shared.Browsers.Actions.Common;
using Shared.Browsers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Gui
{
    static class BrowserManager
    {
        private static Dictionary<BrowserNames, LuckyBrowser> _browsers = new Dictionary<BrowserNames, LuckyBrowser>();
        private static readonly Dictionary<BrowserNames, string> _browserUrls= new Dictionary<BrowserNames, string> {
            {BrowserNames.Auth, "package://auth/index.html"}             
        };
        private static string _currentPage;


        public static bool IsAnyPageOpened
        {
            get
            {
                return _currentPage != null;
            }
        }      
        
        public static bool OpenAuthPage(AuthPages page)
        {
            if (IsAnyPageOpened) 
                return false;
            _currentPage = page.ToString();
             Dispatch(new SetPageAction(BrowserNames.Auth, _currentPage));
            return true;
        }

        public static void ProcCallbackAuth(string key, string data)
        {
            AuthBrowser.ExecuteJs($"serverProcCallback({key}, {data})");
        }

        public static void Dispatch<T>(BrowserActionBase<T> action)
        {
            var browser = GetByName(action.BrowserName);
            browser.Dispatch(action);
        }

        private static LuckyBrowser GetByName(BrowserNames name)
        {
            if (_browsers.ContainsKey(name))
                _browsers.Add(name, new LuckyBrowser(_browserUrls[name]));
            return _browsers[name];
        }

        private static LuckyBrowser AuthBrowser
        {
            get
            {
                return GetByName(BrowserNames.Auth);
            }
        }
    }
}
