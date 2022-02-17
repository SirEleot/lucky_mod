using Client.Core.Entities;
using MessagePack;
using MessagePack.Resolvers;
using RAGE;
using RAGE.Elements;
using RAGE.Ui;
using System;

namespace Client
{
    class Lucky: Events.Script
    {
        public static Action<HtmlWindow> OnBrowserCreated;
        public static LuckyPlayer LocalPlayer
        {
            get { return Player.LocalPlayer as LuckyPlayer; }
        }
        public Lucky()
        {
            RAGE.Util.MsgPack.DefaultOptions = MessagePackSerializerOptions.Standard.WithResolver(StandardResolverAllowPrivate.Instance);

            Entities.Players.CreateEntity = (id, remoteId) => new LuckyPlayer(id, remoteId);
            Player.RecreateLocal();
            Events.OnBrowserCreated += BrowserCreated;
        }

        private void BrowserCreated(HtmlWindow window)
        {
            OnBrowserCreated?.Invoke(window);
        }
    }
}
