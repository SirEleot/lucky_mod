using Shared.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MessagePack;
using System.Linq;

namespace Server.Core.Models
{
    class LuckyAccount
    {
        public LuckyAccount()
        {

        }
        public LuckyAccount(ulong socialId, string login, string email)
        {
            SocialId = socialId;
            Email = email;
            Login = login;
            Characters = new List<LuckyCharacter>();
        }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public ulong SocialId { get; set; }
        public List<LuckyCharacter> Characters { get; set; }

        private int _currentCharacterId;
        [NotMapped]
        public LuckyCharacter CurrentCharacter { 
            get { return Characters?[_currentCharacterId] ?? null; } 
            set { _currentCharacterId = Characters.IndexOf(value); } 
        }

        internal byte[] GetCharacterListData()
        {
            var data = Characters.Select(c => c.GetCharacterDTO()).ToList();
            return MessagePackSerializer.Serialize(data);
        }
    }
}
