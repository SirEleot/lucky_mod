using GTANetworkAPI;
using Server.Core.Entities;
using Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Server.Core.Models;
using Newtonsoft.Json;

namespace Server
{
    class Lucky: Script
    {
        private static List<ILuckyEventManager> _eventManagers = new List<ILuckyEventManager>();
        private static Action OnResourceStart;
        public static LuckySettingsModel LuckySettings { get; private set; }
        public static Action<LuckyPlayer> OnPlayerConnected;
        public static Action<LuckyPlayer> OnPlayerDisconnected;

        public Lucky()
        {
            using var r = new StreamReader("settings.json");
            LuckySettings = JsonConvert.DeserializeObject<LuckySettingsModel>(r.ReadToEnd());
            OnResourceStart += InitLuckyEntities;
            _eventManagers.AddRange(GetInstancesOfImplementingTypes<ILuckyEventManager>().OrderBy(m=>m.Priority));
            foreach (var item in _eventManagers)
            {
                item.OnManagerCreate();
            }
            OnResourceStart += () => {
                foreach (var item in _eventManagers)
                {
                    item.OnServerStart();
                }
            };
        }

        [ServerEvent(Event.PlayerConnected)]
        public void PlayerConnected(LuckyPlayer player)
        {
            OnPlayerConnected?.Invoke(player);
        }

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStarted()
        {
            OnResourceStart?.Invoke();
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void PlayerDisconnected(LuckyPlayer player, DisconnectionType type, string reason)
        {
            player.Loggined = false;
            player.Account = null;
            OnPlayerDisconnected?.Invoke(player);
        }


        private static IEnumerable<T> GetInstancesOfImplementingTypes<T>()
        {
            AppDomain app = AppDomain.CurrentDomain;
            var ass = app.GetAssemblies();
            Type[] types;
            Type targetType = typeof(T);

            foreach (Assembly a in ass)
            {
                types = a.GetTypes();
                foreach (Type t in types)
                {
                    if (t.IsInterface) continue;
                    if (t.IsAbstract) continue;
                    foreach (Type iface in t.GetInterfaces())
                    {
                        if (!iface.Equals(targetType)) continue;
                        yield return (T)Activator.CreateInstance(t);
                        break;
                    }
                }
            }
        }
        private static void InitLuckyEntities()
        {
            RAGE.Entities.Players.CreateEntity = netHandle => new LuckyPlayer(netHandle);
        }
    }
}
