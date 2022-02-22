using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events
{
    public static class AuthorizationEventNames
    {
        /// <summary>
        /// client events
        /// </summary>
        public const string ClientBeginePlayerAuth = "auth:player:begine";
        public const string ClientAuthToCharacterSelect = "auth:character:select";
        public const string ClientAuthSocialWrong = "auth:social:wrong";

        /// <summary>
        /// client events from cef
        /// </summary>
        public const string CefClientAuthComplite = "cef:auth:complite";
        public const string CefClientAuthCloseBrowser = "cef:auth:browser:close";
        public const string CefClientAuthTokenReset = "cef:auth:token:reset";
        public const string CefClientAuthUpdateToken = "cef:auth:token:update";
        public const string CefClientAuthCharacterSelect = "cef:auth:character:select";
        public const string CefClientAuthChatacterCreate = "cef:auth:character:create";

        /// <summary>
        /// server events
        /// </summary>
        public const string ServerRequestCharaterCreate = "auth:character:create:request";
        public const string ServerCharaterSelect = "auth:character:select";


        /// <summary>
        /// server procs
        /// </summary>
        public const string ServerProcCheckToken = "auth:player:token:check";
    }
}
