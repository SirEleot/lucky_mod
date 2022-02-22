using Shared.Browsers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers.Actions.Authorization
{
    class WrongSocialAction : BrowserActionBase<object>
    {
        public WrongSocialAction() : base(BrowserNames.Auth, "setWrongSocial", null) { }

        protected override string SetPayload(object payload)
        {
            return "";
        }
    }
}
