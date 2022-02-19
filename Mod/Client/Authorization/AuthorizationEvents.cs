using Client.Utils;
using RAGE;
using RAGE.Ui;
using Shared.Authorization.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client.Authorization
{
    class AuthorizationEvents: Events.Script
    {
        public AuthorizationEvents()
        {
            Input.Bind(VirtualKeys.OEM3, false, SwitchCursor);
            Events.Add(AuthEventNames.ClientBeginePlayerAuth, AuthorizationService.BeginePlayerAuth);
            Events.Add(AuthEventNames.ClientAuthSocialWrong, AuthorizationService.WrongSocialId);
            Events.Add(AuthEventNames.ClientAuthTokenReset, AuthorizationService.ResetToken);
            Events.Add(AuthEventNames.ClientAuthUpdateToken, AuthorizationService.UpdateToken);
            Events.Add(AuthEventNames.ClientAuthCharacterSelect, AuthorizationService.DoCharacterSelect);
        }

        private void SwitchCursor()
        {
            Cursor.Visible = !Cursor.Visible;
        }
    }
}
