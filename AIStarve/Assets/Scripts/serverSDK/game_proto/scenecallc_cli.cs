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
            _client_handle.modulemanager.add_mothed("scene_client_remove_scene_info", remove_scene_info);
            _client_handle.modulemanager.add_mothed("scene_client_building_scene_info", building_scene_info);
            _client_handle.modulemanager.add_mothed("scene_client_scene_info", scene_info);
            _client_handle.modulemanager.add_mothed("scene_client_move", move);
        }

        public event Action<List<Building>, List<Fence>> on_remove_scene_info;
        public void remove_scene_info(IList<MsgPack.MessagePackObject> inArray){
            var _buildings = new List<Building>();
            var _protocol_arraybuildings = ((MsgPack.MessagePackObject)inArray[0]).AsList();
            foreach (var v_4435b032_5f03_5920_bcc2_f3356d041cc1 in _protocol_arraybuildings){
                _buildings.Add(Building.protcol_to_Building(((MsgPack.MessagePackObject)v_4435b032_5f03_5920_bcc2_f3356d041cc1).AsDictionary()));
            }
            var _fences = new List<Fence>();
            var _protocol_arrayfences = ((MsgPack.MessagePackObject)inArray[1]).AsList();
            foreach (var v_56d10c41_4026_53fb_9ea6_b39368068a37 in _protocol_arrayfences){
                _fences.Add(Fence.protcol_to_Fence(((MsgPack.MessagePackObject)v_56d10c41_4026_53fb_9ea6_b39368068a37).AsDictionary()));
            }
            if (on_remove_scene_info != null){
                on_remove_scene_info(_buildings, _fences);
            }
        }

        public event Action<List<Building>, List<Fence>> on_building_scene_info;
        public void building_scene_info(IList<MsgPack.MessagePackObject> inArray){
            var _buildings = new List<Building>();
            var _protocol_arraybuildings = ((MsgPack.MessagePackObject)inArray[0]).AsList();
            foreach (var v_4435b032_5f03_5920_bcc2_f3356d041cc1 in _protocol_arraybuildings){
                _buildings.Add(Building.protcol_to_Building(((MsgPack.MessagePackObject)v_4435b032_5f03_5920_bcc2_f3356d041cc1).AsDictionary()));
            }
            var _fences = new List<Fence>();
            var _protocol_arrayfences = ((MsgPack.MessagePackObject)inArray[1]).AsList();
            foreach (var v_56d10c41_4026_53fb_9ea6_b39368068a37 in _protocol_arrayfences){
                _fences.Add(Fence.protcol_to_Fence(((MsgPack.MessagePackObject)v_56d10c41_4026_53fb_9ea6_b39368068a37).AsDictionary()));
            }
            if (on_building_scene_info != null){
                on_building_scene_info(_buildings, _fences);
            }
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
