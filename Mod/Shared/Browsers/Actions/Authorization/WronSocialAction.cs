using Shared.Browsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers.Actions.Authorization
{
    class WronSocialAction : IBrowserAction
    {

        private readonly string _action = "setWrongSocial";
        
        public string GetActionName()
        {
            return _action;
        }

        public string GetPayloadData()
        {
            return null;
        }
    }
}
