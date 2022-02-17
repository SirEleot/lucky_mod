using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers.Interfaces
{
    public interface IBrowserAction
    {
        public string GetActionName();
        public string GetPayloadData();
    }
}
