using Client.Authorization;
using RAGE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.Utils
{
    class UtilsEvents : Events.Script
    {
        public UtilsEvents()
        {
            Events.Add("common:server:proc:call", ServerProcServer);
            Events.Add("rpc:cb:auth", AuthorizationService.RpcCallback);
        }

        private async void ServerProcServer(object[] args)
        {
            var procName = args[0].ToString();
            var callback = args[1].ToString();
            var key = Convert.ToInt32(args[2]);
            var result = await Events.CallRemoteProc(procName,  key);
            Events.CallLocal(callback, key, result);
        }
        
    }
}
