using System;
using System.Collections.Generic;
using Abelkhan;
using Newtonsoft.Json.Linq;

namespace Server
{
    class scene_mgr_msg_handle
    {
        private scene_mgr_module scene_mgr_Module = new scene_mgr_module();

        public scene_mgr_msg_handle()
        {
            scene_mgr_Module.on_get_scene_list += Scene_mgr_Module_on_get_scene_list;
            scene_mgr_Module.on_create_scene += Scene_mgr_Module_on_create_scene;
            scene_mgr_Module.on_player_level_scene += Scene_mgr_Module_on_player_level_scene;
            scene_mgr_Module.on_player_into_scene += Scene_mgr_Module_on_player_into_scene;
        }

        private async void Scene_mgr_Module_on_player_into_scene(string scene_guid, string scene_name, string userGuid, string userName)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;
            var rsp = scene_mgr_Module.rsp as scene_mgr_player_into_scene_rsp;

            Log.Log.trace("on_player_into_scene begin! uuid:{0} scene_guid:{1}", uuid, scene_guid);
            try
            {
                var scene = Server.SceneMgr.GetScene(scene_guid); 
                if (scene != null)
                {
                    scene.PlayerIntoScene(userGuid, userName, uuid);
                    rsp.rsp(scene.SceneInfo());
                }
                else
                {
                    var token = Guid.NewGuid().ToString();
                    try
                    {
                        await Server.RedisHandle.Lock(RedisHelp.BuildLoadSceneLockKey(), token, 2000);
                        var _scene_info_list = await Server.RedisHandle.GetData<List<scene_hub_info>>(RedisHelp.BuildSceneInfoListKey());
                        foreach (var _info in _scene_info_list)
                        {
                            if (_info.scene_guid == scene_guid && !string.IsNullOrEmpty(_info.scene_hub_id))
                            {
                                rsp.err((int)em_error.scene_in_other_server, _info);
                                return;
                            }
                        }

                        scene = await Server.SceneMgr.LoadScene(scene_name, scene_guid);
                        scene.PlayerIntoScene(userGuid, userName, uuid);
                        rsp.rsp(scene.SceneInfo());


                        foreach (var _info in _scene_info_list)
                        {
                            if (_info.scene_guid == scene_guid)
                            {
                                _info.scene_hub_id = Hub.Hub.name;
                                goto SetSceneInfoList;
                            }
                        }
                        _scene_info_list.Add(new scene_hub_info
                        {
                            scene_name = scene_name,
                            scene_guid = scene.sceneUUID,
                            scene_hub_id = Hub.Hub.name,
                        });
                    SetSceneInfoList:
                        await Server.RedisHandle.SetData(RedisHelp.BuildSceneInfoListKey(), _scene_info_list, RedisHelp.SceneInfoListTimeout);
                    }
                    catch (System.Exception ex)
                    {
                        Log.Log.err($"{ex}");
                        rsp.err((int)em_error.db_error, new scene_hub_info());
                    }
                    finally
                    {
                        await Server.RedisHandle.UnLock(RedisHelp.BuildLoadSceneLockKey(), token);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
                rsp.err((int)em_error.db_error, new scene_hub_info());
            }
        }

        private void Scene_mgr_Module_on_player_level_scene(string scene_guid)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;
            var rsp = scene_mgr_Module.rsp as scene_mgr_player_level_scene_rsp;

            Log.Log.trace("on_player_level_scene begin! uuid:{0} scene_guid:{1}", uuid, scene_guid);
            try
            {
                var scene = Server.SceneMgr.GetScene(scene_guid);
                if (scene != null)
                {
                    scene.PlayerLevelScene(uuid);
                }
                rsp.rsp();
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
                rsp.err((int)em_error.db_error);
            }
        }

        private async void Scene_mgr_Module_on_create_scene(string sceneName)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;
            var rsp = scene_mgr_Module.rsp as scene_mgr_create_scene_rsp;

            Log.Log.trace("on_create_scene begin! uuid:{0} sceneName:{1}", uuid, sceneName);

            var token = Guid.NewGuid().ToString();
            try
            {
                var scene = await Server.SceneMgr.CreateScene(sceneName);
                rsp.rsp(scene.SceneInfo());

                await Server.RedisHandle.Lock(RedisHelp.BuildLoadSceneLockKey(), token, 2000);
                var _scene_info_list = await Server.RedisHandle.GetData<List<scene_hub_info>>(RedisHelp.BuildSceneInfoListKey());
                _scene_info_list.Add(new scene_hub_info
                {
                    scene_name = sceneName,
                    scene_guid = scene.sceneUUID,
                    scene_hub_id = Hub.Hub.name,
                });
                await Server.RedisHandle.SetData(RedisHelp.BuildSceneInfoListKey(), _scene_info_list, RedisHelp.SceneInfoListTimeout);
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
                rsp.err((int)em_error.db_error);
            }
            finally
            {
                await Server.RedisHandle.UnLock(RedisHelp.BuildLoadSceneLockKey(), token);
            }
        }

        private async void Scene_mgr_Module_on_get_scene_list()
        {
            var uuid = Hub.Hub._gates.current_client_uuid;
            var rsp = scene_mgr_Module.rsp as scene_mgr_get_scene_list_rsp;

            Log.Log.trace("on_get_scene_list begin! uuid:{0}", uuid);
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
