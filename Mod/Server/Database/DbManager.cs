using Server.Core.Interfaces;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Server.Database
{
    class DbManager : ILuckyEventManager
    {
        private static Timer _timer;
        public static ServerDbContext Cache { get; private set; }
        public static ServerDbContext NewSereverContext { 
            get {
                return new ServerDbContext();
            } 
        }

        public static async Task AddAsync(object obj)
        {
            var ctx = new ServerDbContext();
            ctx.Add(obj);
            await ctx.SaveChangesAsync();
            Cache.Attach(obj);
        }
        public static void Add(object obj)
        {
            var ctx = new ServerDbContext();
            ctx.Add(obj);
            ctx.SaveChanges();
            Cache.Attach(obj);
        }

        public int Priority => 10;

        public void OnManagerCreate(){}

        public void OnServerStart()
        {
            Cache = new ServerDbContext();
            _timer = new Timer((double)(Lucky.LuckySettings.DbSavePeriodInMinutes * 1000 * 60));
            _timer.Elapsed += SaveDatabase;
            _timer.Start();
        }

        private void SaveDatabase(object sender, ElapsedEventArgs e)
        {
            Cache.SaveChangesAsync();
        }
    }
}
