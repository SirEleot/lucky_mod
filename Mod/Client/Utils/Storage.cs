using RAGE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RAGE.Ui;
using RAGE.Game;
using Shared.Events;

namespace Client.Utils
{
    class Storage: Events.Script
    {
        private static Dictionary<int, string> _responces = new Dictionary<int, string>();
        private static int _usedToken = 0;
        public Storage()
        {
            Events.Add(StorageEventNames.ClientStorageResponceGet, StorageGetResponce);
        }


        private void StorageGetResponce(object[] args)
        {
            lock (_responces)
            {
                _responces[(int)args[0]] = (string)args[1];
            }
        }

        public static async Task<string> Get(string key)
        {
            var token = ++_usedToken;
            Events.CallLocal(StorageEventNames.ClientStorageRequestGet, token, key);

            while (!_responces.ContainsKey(token))
            {
                await System.Threading.Tasks.Task.Delay(10);
            }
            Invoker.Wait(0);
            return _responces[token];
        }
        public static async Task<T> Get<T>(string key)
        {
            var resulr = await Get(key);
            return JsonConvert.DeserializeObject<T>(resulr);
        }

        public static void Set(string key, string data)
        {
            Events.CallLocal(StorageEventNames.ClientStorageRequestSet, key, data);
        }

        public static void Remove(string key)
        {
            Events.CallLocal(StorageEventNames.ClientStorageRequestRemove, key);
        }
    }
}
