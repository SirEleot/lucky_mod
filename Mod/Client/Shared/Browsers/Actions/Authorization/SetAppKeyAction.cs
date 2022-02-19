using Shared.Browsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers.Actions.Authorization
{
    class SetAppKeyAction : IBrowserAction
    {

        private readonly string _action = "setAppKey";
        private readonly string _appKey;
        public SetAppKeyAction(string appKey)
        {
            _appKey = $"'{appKey}'";
        }
        public string GetActionName()
        {
            return _action;
        }

        public string GetPayloadData()
        {
            return _appKey;
        }
    }
}
