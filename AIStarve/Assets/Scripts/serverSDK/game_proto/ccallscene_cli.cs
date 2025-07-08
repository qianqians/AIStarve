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
    public class scene_mgr_get_scene_list_cb
    {
        private UInt64 cb_uuid;
        private scene_mgr_rsp_cb module_rsp_cb;

        public scene_mgr_get_scene_list_cb(UInt64 _cb_uuid, scene_mgr_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<List<scene_hub_info>> on_get_scene_list_cb;
        public event Action<Int32> on_get_scene_list_err;
        public event Action on_get_scene_list_timeout;

        public scene_mgr_get_scene_list_cb callBack(Action<List<scene_hub_info>> cb, Action<Int32> err)
        {
            on_get_scene_list_cb += cb;
            on_get_scene_list_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.get_scene_list_timeout(cb_uuid);
            });
            on_get_scene_list_timeout += timeout_cb;
        }

        public void call_cb(List<scene_hub_info> info)
        {
            if (on_get_scene_list_cb != null)
            {
                on_get_scene_list_cb(info);
            }
        }

        public void call_err(Int32 errCode)
        {
            if (on_get_scene_list_err != null)
            {
                on_get_scene_list_err(errCode);
            }
        }

        public void call_timeout()
        {
            if (on_get_scene_list_timeout != null)
            {
                on_get_scene_list_timeout();
            }
        }

    }

    public class scene_mgr_create_scene_cb
    {
        private UInt64 cb_uuid;
        private scene_mgr_rsp_cb module_rsp_cb;

        public scene_mgr_create_scene_cb(UInt64 _cb_uuid, scene_mgr_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<SceneInfo> on_create_scene_cb;
        public event Action<Int32> on_create_scene_err;
        public event Action on_create_scene_timeout;

        public scene_mgr_create_scene_cb callBack(Action<SceneInfo> cb, Action<Int32> err)
        {
            on_create_scene_cb += cb;
            on_create_scene_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.create_scene_timeout(cb_uuid);
            });
            on_create_scene_timeout += timeout_cb;
        }

        public void call_cb(SceneInfo info)
        {
            if (on_create_scene_cb != null)
            {
                on_create_scene_cb(info);
            }
        }

        public void call_err(Int32 errCode)
        {
            if (on_create_scene_err != null)
            {
                on_create_scene_err(errCode);
            }
        }

        public void call_timeout()
        {
            if (on_create_scene_timeout != null)
            {
                on_create_scene_timeout();
            }
        }

    }

    public class scene_mgr_player_level_scene_cb
    {
        private UInt64 cb_uuid;
        private scene_mgr_rsp_cb module_rsp_cb;

        public scene_mgr_player_level_scene_cb(UInt64 _cb_uuid, scene_mgr_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action on_player_level_scene_cb;
        public event Action<Int32> on_player_level_scene_err;
        public event Action on_player_level_scene_timeout;

        public scene_mgr_player_level_scene_cb callBack(Action cb, Action<Int32> err)
        {
            on_player_level_scene_cb += cb;
            on_player_level_scene_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.player_level_scene_timeout(cb_uuid);
            });
            on_player_level_scene_timeout += timeout_cb;
        }

        public void call_cb()
        {
            if (on_player_level_scene_cb != null)
            {
                on_player_level_scene_cb();
            }
        }

        public void call_err(Int32 errCode)
        {
            if (on_player_level_scene_err != null)
            {
                on_player_level_scene_err(errCode);
            }
        }

        public void call_timeout()
        {
            if (on_player_level_scene_timeout != null)
            {
                on_player_level_scene_timeout();
            }
        }

    }

    public class scene_mgr_player_into_scene_cb
    {
        private UInt64 cb_uuid;
        private scene_mgr_rsp_cb module_rsp_cb;

        public scene_mgr_player_into_scene_cb(UInt64 _cb_uuid, scene_mgr_rsp_cb _module_rsp_cb)
        {
            cb_uuid = _cb_uuid;
            module_rsp_cb = _module_rsp_cb;
        }

        public event Action<SceneInfo> on_player_into_scene_cb;
        public event Action<Int32, scene_hub_info> on_player_into_scene_err;
        public event Action on_player_into_scene_timeout;

        public scene_mgr_player_into_scene_cb callBack(Action<SceneInfo> cb, Action<Int32, scene_hub_info> err)
        {
            on_player_into_scene_cb += cb;
            on_player_into_scene_err += err;
            return this;
        }

        public void timeout(UInt64 tick, Action timeout_cb)
        {
            TinyTimer.add_timer(tick, ()=>{
                module_rsp_cb.player_into_scene_timeout(cb_uuid);
            });
            on_player_into_scene_timeout += timeout_cb;
        }

        public void call_cb(SceneInfo info)
        {
            if (on_player_into_scene_cb != null)
            {
                on_player_into_scene_cb(info);
            }
        }

        public void call_err(Int32 errCode, scene_hub_info hub_info)
        {
            if (on_player_into_scene_err != null)
            {
                on_player_into_scene_err(errCode, hub_info);
            }
        }

        public void call_timeout()
        {
            if (on_player_into_scene_timeout != null)
            {
                on_player_into_scene_timeout();
            }
        }

    }

/*this cb code is codegen by abelkhan for c#*/
    public class scene_mgr_rsp_cb : Common.IModule {
        public Dictionary<UInt64, scene_mgr_get_scene_list_cb> map_get_scene_list;
        public Dictionary<UInt64, scene_mgr_create_scene_cb> map_create_scene;
        public Dictionary<UInt64, scene_mgr_player_level_scene_cb> map_player_level_scene;
        public Dictionary<UInt64, scene_mgr_player_into_scene_cb> map_player_into_scene;
        public scene_mgr_rsp_cb(Common.ModuleManager modules)
        {
            map_get_scene_list = new Dictionary<UInt64, scene_mgr_get_scene_list_cb>();
            modules.add_mothed("scene_mgr_rsp_cb_get_scene_list_rsp", get_scene_list_rsp);
            modules.add_mothed("scene_mgr_rsp_cb_get_scene_list_err", get_scene_list_err);
            map_create_scene = new Dictionary<UInt64, scene_mgr_create_scene_cb>();
            modules.add_mothed("scene_mgr_rsp_cb_create_scene_rsp", create_scene_rsp);
            modules.add_mothed("scene_mgr_rsp_cb_create_scene_err", create_scene_err);
            map_player_level_scene = new Dictionary<UInt64, scene_mgr_player_level_scene_cb>();
            modules.add_mothed("scene_mgr_rsp_cb_player_level_scene_rsp", player_level_scene_rsp);
            modules.add_mothed("scene_mgr_rsp_cb_player_level_scene_err", player_level_scene_err);
            map_player_into_scene = new Dictionary<UInt64, scene_mgr_player_into_scene_cb>();
            modules.add_mothed("scene_mgr_rsp_cb_player_into_scene_rsp", player_into_scene_rsp);
            modules.add_mothed("scene_mgr_rsp_cb_player_into_scene_err", player_into_scene_err);
        }

        public void get_scene_list_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _info = new List<scene_hub_info>();
            var _protocol_arrayinfo = ((MsgPack.MessagePackObject)inArray[1]).AsList();
            foreach (var v_d856b000_56e0_5f62_a6f3_e6a0c7859745 in _protocol_arrayinfo){
                _info.Add(scene_hub_info.protcol_to_scene_hub_info(((MsgPack.MessagePackObject)v_d856b000_56e0_5f62_a6f3_e6a0c7859745).AsDictionary()));
            }
            var rsp = try_get_and_del_get_scene_list_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_info);
            }
        }

        public void get_scene_list_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _errCode = ((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_get_scene_list_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_errCode);
            }
        }

        public void get_scene_list_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_get_scene_list_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private scene_mgr_get_scene_list_cb try_get_and_del_get_scene_list_cb(UInt64 uuid){
            lock(map_get_scene_list)
            {
                if (map_get_scene_list.TryGetValue(uuid, out scene_mgr_get_scene_list_cb rsp))
                {
                    map_get_scene_list.Remove(uuid);
                }
                return rsp;
            }
        }

        public void create_scene_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _info = SceneInfo.protcol_to_SceneInfo(((MsgPack.MessagePackObject)inArray[1]).AsDictionary());
            var rsp = try_get_and_del_create_scene_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_info);
            }
        }

        public void create_scene_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _errCode = ((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_create_scene_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_errCode);
            }
        }

        public void create_scene_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_create_scene_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private scene_mgr_create_scene_cb try_get_and_del_create_scene_cb(UInt64 uuid){
            lock(map_create_scene)
            {
                if (map_create_scene.TryGetValue(uuid, out scene_mgr_create_scene_cb rsp))
                {
                    map_create_scene.Remove(uuid);
                }
                return rsp;
            }
        }

        public void player_level_scene_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var rsp = try_get_and_del_player_level_scene_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb();
            }
        }

        public void player_level_scene_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _errCode = ((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var rsp = try_get_and_del_player_level_scene_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_errCode);
            }
        }

        public void player_level_scene_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_player_level_scene_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private scene_mgr_player_level_scene_cb try_get_and_del_player_level_scene_cb(UInt64 uuid){
            lock(map_player_level_scene)
            {
                if (map_player_level_scene.TryGetValue(uuid, out scene_mgr_player_level_scene_cb rsp))
                {
                    map_player_level_scene.Remove(uuid);
                }
                return rsp;
            }
        }

        public void player_into_scene_rsp(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _info = SceneInfo.protcol_to_SceneInfo(((MsgPack.MessagePackObject)inArray[1]).AsDictionary());
            var rsp = try_get_and_del_player_into_scene_cb(uuid);
            if (rsp != null)
            {
                rsp.call_cb(_info);
            }
        }

        public void player_into_scene_err(IList<MsgPack.MessagePackObject> inArray){
            var uuid = ((MsgPack.MessagePackObject)inArray[0]).AsUInt64();
            var _errCode = ((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            var _hub_info = scene_hub_info.protcol_to_scene_hub_info(((MsgPack.MessagePackObject)inArray[2]).AsDictionary());
            var rsp = try_get_and_del_player_into_scene_cb(uuid);
            if (rsp != null)
            {
                rsp.call_err(_errCode, _hub_info);
            }
        }

        public void player_into_scene_timeout(UInt64 cb_uuid){
            var rsp = try_get_and_del_player_into_scene_cb(cb_uuid);
            if (rsp != null){
                rsp.call_timeout();
            }
        }

        private scene_mgr_player_into_scene_cb try_get_and_del_player_into_scene_cb(UInt64 uuid){
            lock(map_player_into_scene)
            {
                if (map_player_into_scene.TryGetValue(uuid, out scene_mgr_player_into_scene_cb rsp))
                {
                    map_player_into_scene.Remove(uuid);
                }
                return rsp;
            }
        }

    }

    public class scene_mgr_caller {
        public static scene_mgr_rsp_cb rsp_cb_scene_mgr_handle = null;
        private ThreadLocal<scene_mgr_hubproxy> _hubproxy;
        public Client.Client _client_handle;
        public scene_mgr_caller(Client.Client client_handle_) 
        {
            _client_handle = client_handle_;
            if (rsp_cb_scene_mgr_handle == null)
            {
                rsp_cb_scene_mgr_handle = new scene_mgr_rsp_cb(_client_handle.modulemanager);
            }

            _hubproxy = new ThreadLocal<scene_mgr_hubproxy>();
        }

        public scene_mgr_hubproxy get_hub(string hub_name)
        {
            if (_hubproxy.Value == null)
{
                _hubproxy.Value = new scene_mgr_hubproxy(_client_handle, rsp_cb_scene_mgr_handle);
            }
            _hubproxy.Value.hub_name_38a10f62_3f3c_3cc5_80b0_664977cfd157 = hub_name;
            return _hubproxy.Value;
        }

    }

    public class scene_mgr_hubproxy {
        public string hub_name_38a10f62_3f3c_3cc5_80b0_664977cfd157;
        private Int32 uuid_38a10f62_3f3c_3cc5_80b0_664977cfd157 = (Int32)RandomUUID.random();

        public Client.Client _client_handle;
        public scene_mgr_rsp_cb rsp_cb_scene_mgr_handle;

        public scene_mgr_hubproxy(Client.Client client_handle_, scene_mgr_rsp_cb rsp_cb_scene_mgr_handle_)
        {
            _client_handle = client_handle_;
            rsp_cb_scene_mgr_handle = rsp_cb_scene_mgr_handle_;
        }

        public scene_mgr_get_scene_list_cb get_scene_list(){
            var uuid_d6c8da40_286e_5202_a23d_8acd3dc18a50 = (UInt64)Interlocked.Increment(ref uuid_38a10f62_3f3c_3cc5_80b0_664977cfd157);

            var _argv_c44290a6_e88a_3536_86ce_be8217f21cdc = new ArrayList();
            _argv_c44290a6_e88a_3536_86ce_be8217f21cdc.Add(uuid_d6c8da40_286e_5202_a23d_8acd3dc18a50);
            _client_handle.call_hub(hub_name_38a10f62_3f3c_3cc5_80b0_664977cfd157, "scene_mgr_get_scene_list", _argv_c44290a6_e88a_3536_86ce_be8217f21cdc);

            var cb_get_scene_list_obj = new scene_mgr_get_scene_list_cb(uuid_d6c8da40_286e_5202_a23d_8acd3dc18a50, rsp_cb_scene_mgr_handle);
            lock(rsp_cb_scene_mgr_handle.map_get_scene_list)
            {                rsp_cb_scene_mgr_handle.map_get_scene_list.Add(uuid_d6c8da40_286e_5202_a23d_8acd3dc18a50, cb_get_scene_list_obj);
            }            return cb_get_scene_list_obj;
        }

        public scene_mgr_create_scene_cb create_scene(string scene_name){
            var uuid_18b2cce0_96f0_5a1b_b662_f2f23c8580ba = (UInt64)Interlocked.Increment(ref uuid_38a10f62_3f3c_3cc5_80b0_664977cfd157);

            var _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7 = new ArrayList();
            _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7.Add(uuid_18b2cce0_96f0_5a1b_b662_f2f23c8580ba);
            _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7.Add(scene_name);
            _client_handle.call_hub(hub_name_38a10f62_3f3c_3cc5_80b0_664977cfd157, "scene_mgr_create_scene", _argv_44a7c36e_64cf_31e1_821c_eb7b2ed69ec7);

            var cb_create_scene_obj = new scene_mgr_create_scene_cb(uuid_18b2cce0_96f0_5a1b_b662_f2f23c8580ba, rsp_cb_scene_mgr_handle);
            lock(rsp_cb_scene_mgr_handle.map_create_scene)
            {                rsp_cb_scene_mgr_handle.map_create_scene.Add(uuid_18b2cce0_96f0_5a1b_b662_f2f23c8580ba, cb_create_scene_obj);
            }            return cb_create_scene_obj;
        }

        public scene_mgr_player_level_scene_cb player_level_scene(string scene_guid){
            var uuid_08c576a4_289e_5738_842a_5e8ce11e0c3d = (UInt64)Interlocked.Increment(ref uuid_38a10f62_3f3c_3cc5_80b0_664977cfd157);

            var _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40 = new ArrayList();
            _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40.Add(uuid_08c576a4_289e_5738_842a_5e8ce11e0c3d);
            _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40.Add(scene_guid);
            _client_handle.call_hub(hub_name_38a10f62_3f3c_3cc5_80b0_664977cfd157, "scene_mgr_player_level_scene", _argv_60e2d084_e2d0_346d_9c06_77f5f8acfa40);

            var cb_player_level_scene_obj = new scene_mgr_player_level_scene_cb(uuid_08c576a4_289e_5738_842a_5e8ce11e0c3d, rsp_cb_scene_mgr_handle);
            lock(rsp_cb_scene_mgr_handle.map_player_level_scene)
            {                rsp_cb_scene_mgr_handle.map_player_level_scene.Add(uuid_08c576a4_289e_5738_842a_5e8ce11e0c3d, cb_player_level_scene_obj);
            }            return cb_player_level_scene_obj;
        }

        public scene_mgr_player_into_scene_cb player_into_scene(string scene_guid, string scene_name, string userGuid, string userName){
            var uuid_eaec9b3f_5343_578b_824c_ade30f7a2362 = (UInt64)Interlocked.Increment(ref uuid_38a10f62_3f3c_3cc5_80b0_664977cfd157);

            var _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4 = new ArrayList();
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(uuid_eaec9b3f_5343_578b_824c_ade30f7a2362);
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(scene_guid);
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(scene_name);
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(userGuid);
            _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4.Add(userName);
            _client_handle.call_hub(hub_name_38a10f62_3f3c_3cc5_80b0_664977cfd157, "scene_mgr_player_into_scene", _argv_1d7f9f94_cc7a_356a_b9fb_36951dd62bc4);

            var cb_player_into_scene_obj = new scene_mgr_player_into_scene_cb(uuid_eaec9b3f_5343_578b_824c_ade30f7a2362, rsp_cb_scene_mgr_handle);
            lock(rsp_cb_scene_mgr_handle.map_player_into_scene)
            {                rsp_cb_scene_mgr_handle.map_player_into_scene.Add(uuid_eaec9b3f_5343_578b_824c_ade30f7a2362, cb_player_into_scene_obj);
            }            return cb_player_into_scene_obj;
        }

    }
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

        public void building(List<Building> buildings, List<Fence> fences){
            var _argv_202540e5_3aa7_324c_85c8_f7da9821e7e6 = new ArrayList();
            var _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9 = new ArrayList();
            foreach(var v_08f36ecb_8228_5356_bb8a_753f8de7b7e3 in buildings){
                _array_98ba47f5_b9b8_3536_9493_f5e2255b10a9.Add(Building.Building_to_protcol(v_08f36ecb_8228_5356_bb8a_753f8de7b7e3));
            }
            _argv_202540e5_3aa7_324c_85c8_f7da9821e7e6.Add(_array_98ba47f5_b9b8_3536_9493_f5e2255b10a9);
            var _array_60122d00_e773_3786_860e_24d7a883c2c6 = new ArrayList();
            foreach(var v_a0e1ef11_65fe_5230_bfee_169cb74ff5e0 in fences){
                _array_60122d00_e773_3786_860e_24d7a883c2c6.Add(Fence.Fence_to_protcol(v_a0e1ef11_65fe_5230_bfee_169cb74ff5e0));
            }
            _argv_202540e5_3aa7_324c_85c8_f7da9821e7e6.Add(_array_60122d00_e773_3786_860e_24d7a883c2c6);
            _client_handle.call_hub(hub_name_d9ff8761_7787_3ada_910b_8924a79ea4fb, "scene_building", _argv_202540e5_3aa7_324c_85c8_f7da9821e7e6);
        }

    }

}
