using System.Collections.Generic;
using Abelkhan;

namespace Server
{
    class login_msg_handle
    {
        private login_module login_Module = new login_module();

        public login_msg_handle()
        {
            login_Module.on_player_login_no_token += Login_Module_on_player_login_no_token;
        }

        private async void Login_Module_on_player_login_no_token(string account)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;
            var rsp = login_Module.rsp as login_player_login_no_token_rsp;

            Log.Log.trace("on_player_login_no_author begin! player account:{0}, uuid:{1}", account, uuid);

            try
            {
                var _scene_info_list = await Server.RedisHandle.GetData<List<scene_hub_info>>(RedisHelp.BuildSceneInfoListKey());
                rsp.rsp(_scene_info_list);
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
                rsp.err((int)em_error.db_error);
            }
        }
    }
}
