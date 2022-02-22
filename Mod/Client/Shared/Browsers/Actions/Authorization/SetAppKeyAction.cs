using Shared.Browsers;
using Shared.Browsers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers.Actions.Authorization
{
    class SetAppKeyAction : BrowserActionBase<string>
    {
        public SetAppKeyAction(string appKey):base(BrowserNames.Auth, "appKey", appKey){}       
       
        protected override string SetPayload(string payload)
        {
            return $"'{payload}'";
        }
    }
}
