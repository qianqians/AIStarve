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

        public void move(SceneInfo info){
            var _argv_33efb72e_9227_32af_a058_169be114a277 = new ArrayList();
            _argv_33efb72e_9227_32af_a058_169be114a277.Add(SceneInfo.SceneInfo_to_protcol(info));
            Hub.Hub._gates.call_client(client_uuid_e2b3f311_3d11_3958_8f94_f53379eb568e, "scene_client_move", _argv_33efb72e_9227_32af_a058_169be114a277);
        }

    }

    public class scene_client_multicast {
        public List<string> client_uuids_e2b3f311_3d11_3958_8f94_f53379eb568e;
        public scene_client_rsp_cb rsp_cb_scene_client_handle;

        public scene_client_multicast(scene_client_rsp_cb rsp_cb_scene_client_handle_)
        {
            rsp_cb_scene_client_handle = rsp_cb_scene_client_handle_;
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
