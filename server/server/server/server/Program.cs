using Abelkhan;

namespace Server
{
    class Server
    {
        public static RedisHandle RedisHandle;
        public static SceneMgr SceneMgr;

        static void Main(string[] args)
		{
            var _hub = new Hub.Hub(args[0], args[1], "login", "fixed");

            if (!Hub.Hub._root_config.has_key("redis_for_mq_pwd"))
            {
                RedisHandle = new RedisHandle(Hub.Hub._root_config.get_value_string("redis_for_cache"), string.Empty);
            }
            else
            {
                RedisHandle = new RedisHandle(Hub.Hub._root_config.get_value_string("redis_for_cache"), Hub.Hub._root_config.get_value_string("redis_for_mq_pwd"));
            }

            HttpClientWrapper.Init();

            SceneMgr = new SceneMgr();
            var _client_msg_handle = new client_msg_handle();

            _hub.on_hubproxy += on_hubproxy;
            _hub.on_hubproxy_reconn += on_hubproxy;

            _hub.on_client_disconnect += _hub_on_client_disconnect;

            _hub.onCloseServer += () => {
                _hub.closeSvr();
            };

            Log.Log.trace("login start ok");

            _hub.run().Wait();
        }

        private static void _hub_on_client_disconnect(string uuid)
        {
            SceneMgr.ClientDisconnect(uuid);
        }

        private static void on_hubproxy(Hub.HubProxy _proxy)
        {
            if (_proxy.type == "player")
            {
                //_player_proxy_mng.reg_player_proxy(_proxy);
            }
        }
    }
}
