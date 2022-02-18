using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Authorization.Constants
{
    public abstract class AuthEventNames
    {
        public const string ClientBeginePlayerAuth = "auth:player:begine";
        public const string ClientAuthCharacterSelect = "auth:character:select";
        public const string ClientAuthSocialWrong = "auth:social:wrong";

        public const string ClientAuthComplite = "cef:auth:complite";
        public const string ClientAuthCloseBrowser = "cef:auth:browser:close";
        public const string ClientAuthTokenReset = "cef:auth:token:reset";
        public const string ClientAuthUpdateToken = "cef:auth:token:update";

        public const string ServerCheckToken = "auth:player:token:check";
    }
}
