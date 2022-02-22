using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Shared.Browsers.Enums;

namespace Shared.Browsers.Actions.Authorization
{
    class ToSelectCharacterAction : BrowserActionBase<List<LuckyCharacterDTO>>
    {
        public ToSelectCharacterAction(List<LuckyCharacterDTO> characters) : base(BrowserNames.Auth, "characterSelect/open", characters) { }

        protected override string SetPayload(List<LuckyCharacterDTO> payload)
        {
            return JsonConvert.SerializeObject(payload);
        }       
    }
}
