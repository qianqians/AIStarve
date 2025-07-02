using Abelkhan;

namespace Server
{
    class client_msg_handle
    {
        private login_module login_Module = new login_module();

        public client_msg_handle()
        {
            login_Module.on_player_login_no_token += Login_Module_on_player_login_no_token;
        }

        private async void Login_Module_on_player_login_no_token(string account)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;
            var rsp = login_Module.rsp as login_player_login_no_token_rsp;

            Log.Log.trace("on_player_login_no_author begin! player account:{0}, uuid:{1}", account, uuid);

            var lock_key = RedisHelp.BuildPlayerSvrCacheLockKey(account);
            var token = $"lock_{account}";
            try
            {
                await Server._redis_handle.Lock(lock_key, token, 1000);

                var key = RedisHelp.BuildPlayerSvrCacheKey(account);
                var _player_proxy_name = await Server._redis_handle.GetStrData(key);
                if (string.IsNullOrEmpty(_player_proxy_name))
                {
                    //random_player_svr_rsp(account, rsp);
                }
                else
                {
                    //var _proxy = Login._player_proxy_mng.get_player(_player_proxy_name);
                    //if (_proxy != null)
                    //{
                    //    await Login._redis_handle.Expire(key, RedisHelp.PlayerSvrInfoCacheTimeout);
                    //    //try_player_login(_proxy, account, rsp);
                    //}
                    //else
                    //{
                    //   // random_player_svr_rsp(account, rsp);
                    //}
                }

                var gate_key = RedisHelp.BuildPlayerGateCacheKey(account);
                await Server._redis_handle.SetStrData(gate_key, Hub.Hub._gates.get_client_gate_name(uuid), RedisHelp.PlayerSvrInfoCacheTimeout);
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
                rsp.err((int)em_error.db_error);
            }
            finally
            {
                await Server._redis_handle.UnLock(lock_key, token);
            }
        }
    }
}
