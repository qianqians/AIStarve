using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace Abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

/*this struct code is codegen by abelkhan codegen for c#*/
/*this module code is codegen by abelkhan codegen for c#*/
    public class scene_client_module : Common.IModule {
        public Client.Client _client_handle;
        public scene_client_module(Client.Client client_handle_) 
        {
            _client_handle = client_handle_;
            _client_handle.modulemanager.add_mothed("scene_client_scene_info", scene_info);
            _client_handle.modulemanager.add_mothed("scene_client_move", move);
        }

        public event Action<SceneInfo> on_scene_info;
        public void scene_info(IList<MsgPack.MessagePackObject> inArray){
            var _info = SceneInfo.protcol_to_SceneInfo(((MsgPack.MessagePackObject)inArray[0]).AsDictionary());
            if (on_scene_info != null){
                on_scene_info(_info);
            }
        }

        public event Action<List<UserMoveInfo>> on_move;
        public void move(IList<MsgPack.MessagePackObject> inArray){
            var _info = new List<UserMoveInfo>();
            var _protocol_arrayinfo = ((MsgPack.MessagePackObject)inArray[0]).AsList();
            foreach (var v_d856b000_56e0_5f62_a6f3_e6a0c7859745 in _protocol_arrayinfo){
                _info.Add(UserMoveInfo.protcol_to_UserMoveInfo(((MsgPack.MessagePackObject)v_d856b000_56e0_5f62_a6f3_e6a0c7859745).AsDictionary()));
            }
            if (on_move != null){
                on_move(_info);
            }
        }

    }

}
