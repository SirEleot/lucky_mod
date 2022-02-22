using Shared.Browsers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers.Actions.Authorization
{
    class SetTokenAction : BrowserActionBase<string>
    {
        public SetTokenAction(string token) : base(BrowserNames.Auth, "setToken", token) { }

        protected override string SetPayload(string payload)
        {
            return $"'{payload}'";
        }
      
    }
}
