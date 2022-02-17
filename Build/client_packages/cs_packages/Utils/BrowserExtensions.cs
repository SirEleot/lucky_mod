using RAGE.Ui;
using Shared.Browsers.Actions.Common;
using Shared.Browsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Utils
{
    
    internal static class BrowserExtensions
    {
        public static void Dispatch<T>(this HtmlWindow browser, T action) where T: IBrowserAction
        {
            browser.ExecuteJs($"window.dispatch(\"{action.GetActionName()}\",{action.GetPayloadData()})");
        }
        public static void OpenPage(this HtmlWindow browser, string page)
        {
            Dispatch(browser, new SetPageAction(page));
        }
        public static void Show(this HtmlWindow browser)
        {
            if (!browser.Active) browser.Active = true;
        }
        public static void Hide(this HtmlWindow browser)
        {
            if (browser.Active) browser.Active = false;
        }
    }
}
