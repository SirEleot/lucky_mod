using GTANetworkAPI;
using Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TokenService.Models;

namespace Server.Core.Entities
{
    class LuckyPlayer : Player
    {
        public LuckyPlayer(NetHandle handle) : base(handle)
        {
        }
        public bool Loggined { get; set; }
        public LuckyAccount Account { get; set; }
        public LuckyCharacter Character { get { return Account.CurrentCharacter; } }
        public TokenModel AccessToken { get; set; }

    }
}
