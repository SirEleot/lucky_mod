using Shared.Browsers.Enums;
using System;
using System.Collections.Generic;
using System.Text;



namespace Shared.Browsers.Actions.Common
{
    public class SetPageAction : BrowserActionBase<string>
    {
        public SetPageAction(BrowserNames browser, string page) : base(browser, "setPage", page) { }

        protected override string SetPayload(string payload)
        {
            return $"'{payload}'";
        }           
    }
}
