using Client.Core.Entities;
using Client.Gui;
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
        private static bool _isLoading = false;
        public static Action<HtmlWindow> OnBrowserCreated;
        public static bool IsLoading { 
            get { 
                return _isLoading; 
            } 

            set
            {
                if (value == _isLoading) return;
                _isLoading = value;
                if (_isLoading)
                {
                    if (Cursor.Visible)
                        Cursor.Visible = false;
                }
                else
                {
                    if (BrowserManager.IsAnyPageOpened)
                        Cursor.Visible = true;
                }
            }
        }
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
