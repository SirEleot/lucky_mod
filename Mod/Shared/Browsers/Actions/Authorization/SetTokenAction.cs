using Shared.Browsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers.Actions.Authorization
{
    class SetTokenAction : IBrowserAction
    {
        private readonly string _action = "setToken";
        private readonly string _token;
        public SetTokenAction(string token)
        {
            _token = $"'{token}'";
        }
        public string GetActionName()
        {
            return _action;
        }

        public string GetPayloadData()
        {
            return _token;
        }
    }
}
