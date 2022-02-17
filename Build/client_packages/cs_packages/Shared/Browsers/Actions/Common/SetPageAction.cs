using Shared.Browsers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;



namespace Shared.Browsers.Actions.Common
{
    public class SetPageAction : IBrowserAction
    {
        private readonly string _action = "setPage";
        private readonly string _page;
        public SetPageAction(string page)
        {
            _page = page;
        }

        public string GetActionName()
        {
            return _action;
        }

        public string GetPayloadData()
        {
            return _page;
        }
           
    }
}
