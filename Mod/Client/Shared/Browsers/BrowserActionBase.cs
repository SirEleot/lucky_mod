using Shared.Browsers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Browsers
{
    public abstract class BrowserActionBase<T>
    {
        public BrowserNames BrowserName { get; private set; }
        protected string _action;
        protected string _payload;
        public BrowserActionBase(BrowserNames brouserName, string action, T payload)
        {
            BrowserName = brouserName;
            _action = action;
            _payload = SetPayload(payload);
        }
      
        public string GetActionName() {
            return _action;
        }

        /// <summary>
        /// return serialized payload data
        /// </summary>
        /// <returns>
        /// serialized payload data
        /// </returns>
        public string GetPayloadData() {
            return _payload;
        }

        /// <summary>
        /// serealize payload data for browser
        /// </summary>
        /// <param name="payloadData"></param>
        /// <returns>
        /// serealized payloadData
        /// </returns>
        protected abstract string SetPayload(T payloadData);
    }
}
