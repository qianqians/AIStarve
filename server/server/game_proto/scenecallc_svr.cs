using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace Abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

/*this struct code is codegen by abelkhan codegen for c#*/
/*this caller code is codegen by abelkhan codegen for c#*/
/*this cb code is codegen by abelkhan for c#*/
    public class scene_client_rsp_cb : Common.IModule {
        public scene_client_rsp_cb() 
        {
        }

    }

    public class scene_client_clientproxy {
        public string client_uuid_e2b3f311_3d11_3958_8f94_f53379eb568e;
        private Int32 uuid_e2b3f311_3d11_3958_8f94_f53379eb568e = (Int32)RandomUUID.random();

        public scene_client_rsp_cb rsp_cb_scene_client_handle;

        public scene_client_clientproxy(scene_client_rsp_cb rsp_cb_scene_client_handle_)
        {
            rsp_cb_scene_client_handle = rsp_cb_scene_client_handle_;
        }

        public void scene_info(SceneInfo info){
            var _argv_eaa1df1b_59b5_3e8d_8e20_b592765a38a8 = new ArrayList();
            _argv_eaa1df1b_59b5_3e8d_8e20_b592765a38a8.Add(SceneInfo.SceneInfo_to_protcol(info));
            Hub.Hub._gates.call_client(client_uuid_e2b3f311_3d11_3958_8f94_f53379eb568e, "scene_client_scene_info", _argv_eaa1df1b_59b5_3e8d_8e20_b592765a38a8);
        }

    }

    public class scene_client_multicast {
        public List<string> client_uuids_e2b3f311_3d11_3958_8f94_f53379eb568e;
        public scene_client_rsp_cb rsp_cb_scene_client_handle;

        public scene_client_multicast(scene_client_rsp_cb rsp_cb_scene_client_handle_)
        {
            rsp_cb_scene_client_handle = rsp_cb_scene_client_handle_;
        }

        public void remove_scene_info(List<Building> buildings, List<Fence> fences){
            var _argv_636b2a7a_df6c_3375_b7c3_981255ddfc2d = new ArrayList();
            var _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9 = new ArrayList();
            foreach(var v_08f36ecb_8228_5356_bb8a_753f8de7b7e3 in buildings){
                _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9.Add(Building.Building_to_protcol(v_08f36ecb_8228_5356_bb8a_753f8de7b7e3));
            }
            _argv_636b2a7a_df6c_3375_b7c3_981255ddfc2d.Add(_array_98ba47f5_b9b8_3536_9493_f5e2255b10a9);
            var _array_60122d00_e773_3786_860e_24d7a883c2c6 = new ArrayList();
            foreach(var v_a0e1ef11_65fe_5230_bfee_169cb74ff5e0 in fences){
                _array_60122d00_e773_3786_860e_24d7a883c2c6.Add(Fence.Fence_to_protcol(v_a0e1ef11_65fe_5230_bfee_169cb74ff5e0));
            }
            _argv_636b2a7a_df6c_3375_b7c3_981255ddfc2d.Add(_array_60122d00_e773_3786_860e_24d7a883c2c6);
            Hub.Hub._gates.call_group_client(client_uuids_e2b3f311_3d11_3958_8f94_f53379eb568e, "scene_client_remove_scene_info", _argv_636b2a7a_df6c_3375_b7c3_981255ddfc2d);
        }

        public void building_scene_info(List<Building> buildings, List<Fence> fences){
            var _argv_35b1cb39_ec39_3ac9_80c5_56fa5338db62 = new ArrayList();
            var _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9 = new ArrayList();
            foreach(var v_08f36ecb_8228_5356_bb8a_753f8de7b7e3 in buildings){
                _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9.Add(Building.Building_to_protcol(v_08f36ecb_8228_5356_bb8a_753f8de7b7e3));
            }
            _argv_35b1cb39_ec39_3ac9_80c5_56fa5338db62.Add(_array_98ba47f5_b9b8_3536_9493_f5e2255b10a9);
            var _array_60122d00_e773_3786_860e_24d7a883c2c6 = new ArrayList();
            foreach(var v_a0e1ef11_65fe_5230_bfee_169cb74ff5e0 in fences){
                _array_60122d00_e773_3786_860e_24d7a883c2c6.Add(Fence.Fence_to_protcol(v_a0e1ef11_65fe_5230_bfee_169cb74ff5e0));
            }
            _argv_35b1cb39_ec39_3ac9_80c5_56fa5338db62.Add(_array_60122d00_e773_3786_860e_24d7a883c2c6);
            Hub.Hub._gates.call_group_client(client_uuids_e2b3f311_3d11_3958_8f94_f53379eb568e, "scene_client_building_scene_info", _argv_35b1cb39_ec39_3ac9_80c5_56fa5338db62);
        }

        public void move(List<UserMoveInfo> info){
            var _argv_33efb72e_9227_32af_a058_169be114a277 = new ArrayList();
            var _array_391fd3d4_2d55_3f5e_9223_7f450a814a15 = new ArrayList();
            foreach(var v_0c15545d_d42a_5fe0_bed7_a9496851e88b in info){
                _array_391fd3d4_2d55_3f5e_9223_7f450a814a15.Add(UserMoveInfo.UserMoveInfo_to_protcol(v_0c15545d_d42a_5fe0_bed7_a9496851e88b));
            }
            _argv_33efb72e_9227_32af_a058_169be114a277.Add(_array_391fd3d4_2d55_3f5e_9223_7f450a814a15);
            Hub.Hub._gates.call_group_client(client_uuids_e2b3f311_3d11_3958_8f94_f53379eb568e, "scene_client_move", _argv_33efb72e_9227_32af_a058_169be114a277);
        }

    }

    public class scene_client_broadcast {
        public scene_client_rsp_cb rsp_cb_scene_client_handle;

        public scene_client_broadcast(scene_client_rsp_cb rsp_cb_scene_client_handle_)
        {
            rsp_cb_scene_client_handle = rsp_cb_scene_client_handle_;
        }

    }

    public class scene_client_caller {
        public static scene_client_rsp_cb rsp_cb_scene_client_handle = null;
        private ThreadLocal<scene_client_clientproxy> _clientproxy;
        private ThreadLocal<scene_client_multicast> _multicast;
        private scene_client_broadcast _broadcast;

        public scene_client_caller() 
        {
            if (rsp_cb_scene_client_handle == null)
            {
                rsp_cb_scene_client_handle = new scene_client_rsp_cb();
            }

            _clientproxy = new ThreadLocal<scene_client_clientproxy>();
            _multicast = new ThreadLocal<scene_client_multicast>();
            _broadcast = new scene_client_broadcast(rsp_cb_scene_client_handle);
        }

        public scene_client_clientproxy get_client(string client_uuid) {
            if (_clientproxy.Value == null)
{
                _clientproxy.Value = new scene_client_clientproxy(rsp_cb_scene_client_handle);
            }
            _clientproxy.Value.client_uuid_e2b3f311_3d11_3958_8f94_f53379eb568e = client_uuid;
            return _clientproxy.Value;
        }

        public scene_client_multicast get_multicast(List<string> client_uuids) {
            if (_multicast.Value == null)
{
                _multicast.Value = new scene_client_multicast(rsp_cb_scene_client_handle);
            }
            _multicast.Value.client_uuids_e2b3f311_3d11_3958_8f94_f53379eb568e = client_uuids;
            return _multicast.Value;
        }

        public scene_client_broadcast get_broadcast() {
            return _broadcast;
        }
    }


}
