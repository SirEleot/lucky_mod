using Client.Utils;
using RAGE;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Events;

namespace Client.Authorization
{
    class AuthorizationEvents: Events.Script
    {
        public AuthorizationEvents()
        {
            //bind keys
            Input.Bind(VirtualKeys.OEM3, false, SwitchCursor);

            //init client events
            Events.Add(AuthorizationEventNames.ClientBeginePlayerAuth, AuthorizationService.BeginePlayerAuth);
            Events.Add(AuthorizationEventNames.ClientAuthSocialWrong, AuthorizationService.WrongSocialId);
            Events.Add(AuthorizationEventNames.ClientAuthToCharacterSelect, AuthorizationService.ToCharacterSelect);

            //init cef events
            Events.Add(AuthorizationEventNames.CefClientAuthTokenReset, AuthorizationService.ResetToken);
            Events.Add(AuthorizationEventNames.CefClientAuthUpdateToken, AuthorizationService.UpdateToken);
            Events.Add(AuthorizationEventNames.CefClientAuthCharacterSelect, AuthorizationService.DoCharacterSelect);
            Events.Add(AuthorizationEventNames.CefClientAuthChatacterCreate, AuthorizationService.DoCharacterCreate);
        }

        private void SwitchCursor()
        {
            if (Lucky.IsLoading) return;
            Cursor.Visible = !Cursor.Visible;
        }
    }
}
