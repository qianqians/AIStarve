using System.Collections.Generic;
using Abelkhan;

namespace Server
{
    class scene_msg_handle
    {
        private scene_module scene_Module = new scene_module();

        public scene_msg_handle()
        {
            scene_Module.on_move += Scene_Module_on_move;
            scene_Module.on_remove_building += Scene_Module_on_remove_building;
            scene_Module.on_building += Scene_Module_on_building;
        }

        private void Scene_Module_on_remove_building(List<Building> buildings, List<Fence> fences)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;
            
            Log.Log.trace("on_remove_building begin! uuid:{0}", uuid);
            try
            {
                var scene = Server.SceneMgr.ClientScenes[uuid];
                if (scene != null)
                {
                    scene.PlayerRemoveBuilding(uuid, buildings, fences);
                }
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
            }
        }

        private void Scene_Module_on_building(List<Building> buildings, List<Fence> fences)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;

            Log.Log.trace("on_building begin! uuid:{0}", uuid);
            try
            {
                var scene = Server.SceneMgr.ClientScenes[uuid];
                if (scene != null)
                {
                    scene.PlayerBuilding(uuid, buildings, fences);
                }
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
            }
        }

        private void Scene_Module_on_move(Pos pos, Direction dir)
        {
            var uuid = Hub.Hub._gates.current_client_uuid;

            Log.Log.trace("on_move begin! uuid:{0}", uuid);
            try
            {
                var scene = Server.SceneMgr.ClientScenes[uuid];
                if (scene != null)
                {
                    scene.PlayerMove(uuid, pos, dir);
                }
            }
            catch (System.Exception ex)
            {
                Log.Log.err($"{ex}");
            }
        }
    }
}
