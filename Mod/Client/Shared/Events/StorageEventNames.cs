using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events
{
    public abstract class StorageEventNames
    {
        public const string ClientStorageRequestSet = "storage:request:set";
        public const string ClientStorageRequestGet = "storage:request:get";
        public const string ClientStorageRequestRemove = "storage:request:remove";
        public const string ClientStorageResponceGet = "storage:responce:get";
    }
}
