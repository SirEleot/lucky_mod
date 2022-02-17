using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTO
{
    [MessagePackObject]
    public class LuckyCharacterDTO
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string FirstName { get; set; }
        [Key(2)]
        public string LastName { get; set; }
        [Key(3)]
        public uint Cash { get; set; }
        [Key(4)]
        public uint Bank { get; set; }
    }
}
