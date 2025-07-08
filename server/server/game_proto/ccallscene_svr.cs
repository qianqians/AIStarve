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
    public class scene_mgr_get_scene_list_rsp : Common.Response {
        private string _client_uuid_c44290a6_e88a_3536_86ce_be8217f21cdc;
        private UInt64 uuid_29b8fbfc_39fb_308d_937c_5a53271a0473;
        public scene_mgr_get_scene_list_rsp(string client_uuid, UInt64 _uuid)
        {
            _client_uuid_c44290a6_e88a_3536_86ce_be8217f21cdc = client_uuid;
            uuid_29b8fbfc_39fb_308d_937c_5a53271a0473 = _uuid;
        }

        public void rsp(List<scene_hub_info> info_391fd3d4_2d55_3f5e_9223_7f450a814a15){
            var _argv_c44290a6_e88a_3536_86ce_be8217f21cdc = new ArrayList();
            _argv_c44290a6_e88a_3536_86ce_be8217f21cdc.Add(uuid_29b8fbfc_39fb_308d_937c_5a53271a0473);
            var _array_391fd3d4_2d55_3f5e_9223_7f450a814a15 = new ArrayList();
            foreach(var v_d856b000_56e0_5f62_a6f3_e6a0c7859745 in info_391fd3d4_2d55_3f5e_9223_7f450a814a15){
                _array_391fd3d4_2d55_3f5e_9223_7f450a814a15.Add(scene_hub_info.scene_hub_info_to_protcol(v_d856b000_56e0_5f62_a6f3_e6a0c7859745));
            }
            _argv_c44290a6_e88a_3536_86ce_be8217f21cdc.Add(_array_391fd3d4_2d55_3f5e_9223_7f450a814a15);
            Hub.Hub._gates.call_client(_client_uuid_c44290a6_e88a_3536_86ce_be8217f21cdc, "scene_mgr_rsp_cb_get_scene_list_rsp", _argv_c44290a6_e88a_3536_86ce_be8217f21cdc);
        }

        public void err(Int32 errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec){
            var _argv_c44290a6_e88a_3536_86ce_be8217f21cdc = new ArrayList();
            _argv_c44290a6_e88a_3536_86ce_be8217f21cdc.Add(uuid_29b8fbfc_39fb_308d_937c_5a53271a0473);
            _argv_c44290a6_e88a_3536_86ce_be8217f21cdc.Add(errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec);
            Hub.Hub._gates.call_client(_client_uuid_c44290a6_e88a_3536_86ce_be8217f21cdc, "scene_mgr_rsp_cb_get_scene_list_err", _argv_c44290a6_e88a_3536_86ce_be8217f21cdc);
        }

    }

    public class scene_mgr_create_scene_rsp : Common.Response {
        private string _client_uuid_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7;
        private UInt64 uuid_aedeae4e_6650_3d43_ad1f_28f39ad880fa;
        public scene_mgr_create_scene_rsp(string client_uuid, UInt64 _uuid)
        {
            _client_uuid_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7 = client_uuid;
            uuid_aedeae4e_6650_3d43_ad1f_28f39ad880fa = _uuid;
        }

        public void rsp(SceneInfo info_391fd3d4_2d55_3f5e_9223_7f450a814a15){
            var _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7 = new ArrayList();
            _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7.Add(uuid_aedeae4e_6650_3d43_ad1f_28f39ad880fa);
            _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7.Add(SceneInfo.SceneInfo_to_protcol(info_391fd3d4_2d55_3f5e_9223_7f450a814a15));
            Hub.Hub._gates.call_client(_client_uuid_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7, "scene_mgr_rsp_cb_create_scene_rsp", _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7);
        }

        public void err(Int32 errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec){
            var _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7 = new ArrayList();
            _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7.Add(uuid_aedeae4e_6650_3d43_ad1f_28f39ad880fa);
            _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7.Add(errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec);
            Hub.Hub._gates.call_client(_client_uuid_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7, "scene_mgr_rsp_cb_create_scene_err", _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7);
        }

    }

    public class scene_mgr_player_level_scene_rsp : Common.Response {
        private string _client_uuid_60e2d084_e2d0_346d_9c06_77f5f8acfa40;
        private UInt64 uuid_db8dc61f_ed2e_315f_a4bd_1818a35596c2;
        public scene_mgr_player_level_scene_rsp(string client_uuid, UInt64 _uuid)
        {
            _client_uuid_60e2d084_e2d0_346d_9c06_77f5f8acfa40 = client_uuid;
            uuid_db8dc61f_ed2e_315f_a4bd_1818a35596c2 = _uuid;
        }

        public void rsp(){
            var _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40 = new ArrayList();
            _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40.Add(uuid_db8dc61f_ed2e_315f_a4bd_1818a35596c2);
            Hub.Hub._gates.call_client(_client_uuid_60e2d084_e2d0_346d_9c06_77f5f8acfa40, "scene_mgr_rsp_cb_player_level_scene_rsp", _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40);
        }

        public void err(Int32 errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec){
            var _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40 = new ArrayList();
            _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40.Add(uuid_db8dc61f_ed2e_315f_a4bd_1818a35596c2);
            _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40.Add(errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec);
            Hub.Hub._gates.call_client(_client_uuid_60e2d084_e2d0_346d_9c06_77f5f8acfa40, "scene_mgr_rsp_cb_player_level_scene_err", _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40);
        }

    }

    public class scene_mgr_player_into_scene_rsp : Common.Response {
        private string _client_uuid_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4;
        private UInt64 uuid_2cc6284e_8b8f_32ec_954c_b6da4b42082c;
        public scene_mgr_player_into_scene_rsp(string client_uuid, UInt64 _uuid)
        {
            _client_uuid_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4 = client_uuid;
            uuid_2cc6284e_8b8f_32ec_954c_b6da4b42082c = _uuid;
        }

        public void rsp(SceneInfo info_391fd3d4_2d55_3f5e_9223_7f450a814a15){
            var _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4 = new ArrayList();
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(uuid_2cc6284e_8b8f_32ec_954c_b6da4b42082c);
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(SceneInfo.SceneInfo_to_protcol(info_391fd3d4_2d55_3f5e_9223_7f450a814a15));
            Hub.Hub._gates.call_client(_client_uuid_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4, "scene_mgr_rsp_cb_player_into_scene_rsp", _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4);
        }

        public void err(Int32 errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec, scene_hub_info hub_info_4ca94c1e_3083_3fe9_a4f0_b4f03b01b0f2){
            var _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4 = new ArrayList();
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(uuid_2cc6284e_8b8f_32ec_954c_b6da4b42082c);
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(errCode_c2a4463a_f4dd_322e_bbf5_24a0acf9d2ec);
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(scene_hub_info.scene_hub_info_to_protcol(hub_info_4ca94c1e_3083_3fe9_a4f0_b4f03b01b0f2));
            Hub.Hub._gates.call_client(_client_uuid_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4, "scene_mgr_rsp_cb_player_into_scene_err", _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4);
        }

    }

    public class scene_mgr_module : Common.IModule {
        public scene_mgr_module()
        {
            Hub.Hub._modules.add_mothed("scene_mgr_get_scene_list", get_scene_list);
            Hub.Hub._modules.add_mothed("scene_mgr_create_scene", create_scene);
            Hub.Hub._modules.add_mothed("scene_mgr_player_level_scene", player_level_scene);
            Hub.Hub._modules.add_mothed("scene_mgr_player_into_scene", player_into_scene);
        }

        public event Action on_get_scene_list;
        public void get_scene_list(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            rsp = new scene_mgr_get_scene_list_rsp(Hub.Hub._gates.current_client_uuid, _cb_uuid);
            if (on_get_scene_list != null){
                on_get_scene_list();
            }
            rsp = null;
        }

        public event Action<string> on_create_scene;
        public void create_scene(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _scene_name = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            rsp = new scene_mgr_create_scene_rsp(Hub.Hub._gates.current_client_uuid, _cb_uuid);
            if (on_create_scene != null){
                on_create_scene(_scene_name);
            }
            rsp = null;
        }

        public event Action<string> on_player_level_scene;
        public void player_level_scene(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _scene_guid = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            rsp = new scene_mgr_player_level_scene_rsp(Hub.Hub._gates.current_client_uuid, _cb_uuid);
            if (on_player_level_scene != null){
                on_player_level_scene(_scene_guid);
            }
            rsp = null;
        }

        public event Action<string, string, string, string> on_player_into_scene;
        public void player_into_scene(IList<MsgPack.MessagePackObject> inArray){
            var _cb_uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _scene_guid = ((MsgPack.MessagePackObject)inArray[1]).AsString();
            var _scene_name = ((MsgPack.MessagePackObject)inArray[2]).AsString();
            var _userGuid = ((MsgPack.MessagePackObject)inArray[3]).AsString();
            var _userName = ((MsgPack.MessagePackObject)inArray[4]).AsString();
            rsp = new scene_mgr_player_into_scene_rsp(Hub.Hub._gates.current_client_uuid, _cb_uuid);
            if (on_player_into_scene != null){
                on_player_into_scene(_scene_guid, _scene_name, _userGuid, _userName);
            }
            rsp = null;
        }

    }
    public class scene_module : Common.IModule {
        public scene_module()
        {
            Hub.Hub._modules.add_mothed("scene_move", move);
            Hub.Hub._modules.add_mothed("scene_building", building);
        }

        public event Action<Pos, Direction> on_move;
        public void move(IList<MsgPack.MessagePackObject> inArray){
            var _start = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)inArray[0]).AsDictionary());
            var _dir = (Direction)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            if (on_move != null){
                on_move(_start, _dir);
            }
        }

        public event Action<List<Building>, List<Fence>> on_building;
        public void building(IList<MsgPack.MessagePackObject> inArray){
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
            if (on_building != null){
                on_building(_buildings, _fences);
            }
        }

    }

}
