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
    public class scene_rsp_cb : Common.IModule {
        public scene_rsp_cb(Common.ModuleManager modules)
        {
        }

    }

    public class scene_caller {
        public static scene_rsp_cb rsp_cb_scene_handle = null;
        private ThreadLocal<scene_hubproxy> _hubproxy;
        public Client.Client _client_handle;
        public scene_caller(Client.Client client_handle_) 
        {
            _client_handle = client_handle_;
            if (rsp_cb_scene_handle == null)
            {
                rsp_cb_scene_handle = new scene_rsp_cb(_client_handle.modulemanager);
            }

            _hubproxy = new ThreadLocal<scene_hubproxy>();
        }

        public scene_hubproxy get_hub(string hub_name)
        {
            if (_hubproxy.Value == null)
{
                _hubproxy.Value = new scene_hubproxy(_client_handle, rsp_cb_scene_handle);
            }
            _hubproxy.Value.hub_name_d9ff8761_7787_3ada_910b_8924a79ea4fb = hub_name;
            return _hubproxy.Value;
        }

    }

    public class scene_hubproxy {
        public string hub_name_d9ff8761_7787_3ada_910b_8924a79ea4fb;
        private Int32 uuid_d9ff8761_7787_3ada_910b_8924a79ea4fb = (Int32)RandomUUID.random();

        public Client.Client _client_handle;
        public scene_rsp_cb rsp_cb_scene_handle;

        public scene_hubproxy(Client.Client client_handle_, scene_rsp_cb rsp_cb_scene_handle_)
        {
            _client_handle = client_handle_;
            rsp_cb_scene_handle = rsp_cb_scene_handle_;
        }

        public void move(Pos start, Direction dir){
            var _argv_33efb72e_9227_32af_a058_169be114a277 = new ArrayList();
            _argv_33efb72e_9227_32af_a058_169be114a277.Add(Pos.Pos_to_protcol(start));
            _argv_33efb72e_9227_32af_a058_169be114a277.Add((int)dir);
            _client_handle.call_hub(hub_name_d9ff8761_7787_3ada_910b_8924a79ea4fb, "scene_move", _argv_33efb72e_9227_32af_a058_169be114a277);
        }

        public void building(List<Building> buildings){
            var _argv_202540e5_3aa7_324c_85c8_f7da9821e7e6 = new ArrayList();
            var _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9 = new ArrayList();
            foreach(var v_08f36ecb_8228_5356_bb8a_753f8de7b7e3 in buildings){
                _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9.Add(Building.Building_to_protcol(v_08f36ecb_8228_5356_bb8a_753f8de7b7e3));
            }
            _argv_202540e5_3aa7_324c_85c8_f7da9821e7e6.Add(_array_98ba47f5_b9b8_3536_9493_f5e2255b10a9);
            _client_handle.call_hub(hub_name_d9ff8761_7787_3ada_910b_8924a79ea4fb, "scene_building", _argv_202540e5_3aa7_324c_85c8_f7da9821e7e6);
        }

    }

}
