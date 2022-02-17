using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Interfaces
{
    interface ILuckyEventManager
    {
        public int Priority { get; }
        public void OnManagerCreate();
        public void OnServerStart();
    }
}
