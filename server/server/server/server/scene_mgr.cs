using Abelkhan;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class SceneMgr()
    {
        private Dictionary<string, Scene> scenes = new();
        public Dictionary<string, Scene> Scenes
        {
            get
            {
                return scenes;
            }

            private set { }
        }

        private Dictionary<string, Scene> clientScenes = new();
        public Dictionary<string, Scene> ClientScenes
        {
            get
            {
                return clientScenes;
            }

            private set { }
        }

        public async Task<Scene> CreateScene(string sceneName)
        {
            var scene = await Scene.CreateScene(sceneName);
            scenes.Add(scene.sceneUUID, scene);
            return scene;
        }

        public async Task<Scene> LoadScene(string sceneName, string sceneUUID)
        {
            var scene = await Scene.LoadScene(sceneName, sceneUUID); 
            scenes.Add(scene.sceneUUID, scene);
            return scene;
        }

        public Scene GetScene(string sceneUUID)
        {
            return scenes.GetValueOrDefault(sceneUUID, null);
        }

        public void PlayerIntoScene(string sceneUUID, string userGuid, string userName, string userNetUUID)
        {
            if (scenes.TryGetValue(sceneUUID, out var scene))
            {
                scene.PlayerIntoScene(userGuid, userName, userNetUUID);
                clientScenes[userNetUUID] = scene;
            }
        }

        public void ClientDisconnect(string uuid)
        {
            if (clientScenes.TryGetValue(uuid, out var scene))
            {
                scene.PlayerLevelScene(uuid);
                clientScenes.Remove(uuid);
            }
        }
    }
}