using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Models
{
    class LuckyCharacter
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public LuckyCharacterDTO GetCharacterDTO()
        {
            return new LuckyCharacterDTO { Bank = 0, Cash = 0, FirstName = FirstName, LastName = LastName };
        }
    }
}
