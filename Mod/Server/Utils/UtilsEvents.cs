using GTANetworkAPI;
using Server.Core.Entities;
using Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Utils
{
    class UtilsEvents : Script
    {
        public int Priority => 10;

        public void OnManagerCreate()
        {
            
        }

        public void OnServerStart()
        {
            
        }

        [RemoteProc("testserverproc")]
        public string TestServerProc(LuckyPlayer player)
        {
            return "resule test server proc";
        }
    }
}
