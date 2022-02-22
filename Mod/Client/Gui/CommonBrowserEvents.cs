using Client.Authorization;
using RAGE;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Client.Gui
{
    class CommonBrowserEvents : Events.Script
    {
        public CommonBrowserEvents()
        {
            Events.Add(CommonEventNames.ClientCommonProcCall, ServerProcCall);
            Events.Add(CommonEventNames.ClientCommonAuthProcCallback, AuthorizationService.RpcCallback);
        }

        private async void ServerProcCall(object[] args)
        {
            var callback = args[0].ToString();
            var procName = args[1].ToString();
            var key = Convert.ToInt32(args[2]);
            var result = (args.Length > 3) ? await Events.CallRemoteProc(procName, args[3]) : await Events.CallRemoteProc(procName);
            Events.CallLocal(callback, key, result);
        }
        
    }
}
