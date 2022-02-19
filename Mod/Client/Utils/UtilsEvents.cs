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
            var callback = args[0].ToString();
            var procName = args[1].ToString();
            var key = Convert.ToInt32(args[2]);
            var result = (args.Length > 3) ? await Events.CallRemoteProc(procName, args[3]) : await Events.CallRemoteProc(procName);
            Events.CallLocal(callback, key, result);
        }
        
    }
}
