using RAGE.Ui;
using Shared.Browsers;
using Shared.Browsers.Actions.Common;
using Shared.Browsers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Gui.Models
{
    class LuckyBrowser: HtmlWindow
    {
        public LuckyBrowser(string url): base(url)
        {
        }
       
        public void Dispatch<T>(BrowserActionBase<T> action)
        {
            ExecuteJs($"window.dispatch(\"{action.GetActionName()}\",{action.GetPayloadData()})");
        }       
        public void Show()
        {
            if (Active) 
                Active = true;
        }
        public void Hide()
        {
            if (Active) 
                Active = false;
        }
    }
}
