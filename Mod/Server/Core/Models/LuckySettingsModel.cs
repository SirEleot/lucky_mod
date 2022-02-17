using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Core.Models
{
    class LuckySettingsModel
    {
        public string DbConnectionString { get; set; }
        public int DbSavePeriodInMinutes { get; set; }
        public string PrivateKey { get; set; }
    }
}
