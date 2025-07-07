using Abelkhan;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
    public class PlayerFromDBError : System.Exception
    {
        public string Error;
        public PlayerFromDBError(string err)
        {
            Error = err;
        }
    }

    public class Player
    {
        public UserInformation userInformation = null;
        public List<Fence> fences = [];
        public List<Building> buildings = [];

        private string netUUID = string.Empty;
        public string NetUUID
        {
            get {
                return netUUID;
            }

            set {
                netUUID = value;
            }
        }

        public bool IsOnline
        {
            get
            {
                return netUUID != string.Empty;
            }

            private set {}
        }

        private Hub.DBProxyProxy _dbProxy;

        static private Task<UserInformation> loadUserInformation(Hub.DBProxyProxy _dbProxy, string sceneName, string userGuid)
        {
            var _t = new TaskCompletionSource<UserInformation>();

            var query = new DBQueryHelper();
            query.condition("DataType", "UserInformation");
            query.condition("SceneName", sceneName);
            query.condition("UserGuid", userGuid);
            _dbProxy.getCollection("aiStarve", "game").getObjectInfo(query.query(), (value) => {
                if (value.Count > 1)
                {
                    throw new PlayerFromDBError($"repeated sceneName:{sceneName} userGuid:{userGuid}");
                }
                else if (value.Count == 1)
                {
                    var doc = value[0] as BsonDocument;

                    var _info = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<UserInformation>(doc);
                    _t.SetResult(_info);
                }
                else
                {
                    _t.SetResult(null);
                }
            }, ()=>{ });

            return _t.Task;
        }

        private Task loadFence(string sceneName, string userGuid)
        {
            var _t = new TaskCompletionSource();

            var query = new DBQueryHelper();
            query.condition("DataType", "UserFence");
            query.condition("SceneName", sceneName);
            query.condition("UserGuid", userGuid);
            _dbProxy.getCollection("aiStarve", "game").getObjectInfo(query.query(), (value) => {
                if (value.Count > 0)
                {
                    foreach (var doc in value)
                    {
                        var _fence = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<Fence>(doc as BsonDocument);
                        fences.Add(_fence);
                    }
                }
                else
                {
                }
            }, () => {
                _t.SetResult();
            });

            return _t.Task;
        }

        private Task loadBuilding(string sceneName, string userGuid)
        {
            var _t = new TaskCompletionSource();

            var query = new DBQueryHelper();
            query.condition("DataType", "UserBuilding");
            query.condition("SceneName", sceneName);
            query.condition("UserGuid", userGuid);
            _dbProxy.getCollection("aiStarve", "game").getObjectInfo(query.query(), (value) => {
                if (value.Count > 0)
                {
                    foreach (var doc in value)
                    {
                        var _building = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<Building>(doc as BsonDocument);
                        buildings.Add(_building);
                    }
                }
                else
                {
                }
            }, () => {
                _t.SetResult();
            });

            return _t.Task;
        }

        public static async Task<Player> LoadPlayer(Hub.DBProxyProxy _dbProxy, string sceneName, string userGuid)
        {
            var userInfo = await loadUserInformation(_dbProxy, sceneName, userGuid);
            if (userInfo == null)
            {
                return null;
            }

            var player = new Player();
            player._dbProxy = _dbProxy;
            player.userInformation = userInfo;

            await player.loadFence(sceneName, userGuid);
            await player.loadBuilding(sceneName, userGuid);

            return player;
        }

        public bool CheckInFence(Pos pos)
        {
            return false;
        }
    }

    public class Scene
    {
        public static int Length
        {
            get {
                return 1024;
            }
            private set {}
        }

        public static int Width
        {
            get
            {
                return 1024;
            }
            private set { }
        }

        public static scene_client_caller SceneClientCaller = new scene_client_caller();

        private Dictionary<string, Player> players = new();
        public Dictionary<string, Player> Players { 
            get { 
                return players; 
            } 
        }

        private Dictionary<string, Player> netUUIDPlayers = new();
        public Dictionary<string, Player> NetUUIDPlayers
        {
            get
            {
                return netUUIDPlayers;
            }
        }

        private Hub.DBProxyProxy dbProxy;
        public string sceneName;
        public string sceneUUID;

        private Task<List<string>> loadPlayers()
        {
            var _t = new TaskCompletionSource<List<string>>();

            List<string> list = null;
            var query = new DBQueryHelper();
            query.condition("DataType", "PlayerList");
            query.condition("SceneName", sceneName);
            query.condition("SceneUUID", sceneUUID);
            dbProxy.getCollection("aiStarve", "game").getObjectInfo(query.query(), (value) => {
                if (value.Count > 1)
                {
                    new PlayerFromDBError($"repeated PlayerList sceneName:{sceneName}");
                }
                if (value.Count == 1)
                {
                    var doc = value[0] as BsonDocument;
                    list = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<string>>(doc as BsonDocument);
                    _t.SetResult(list);
                }
                else
                {
                    _t.SetResult(list);
                }
            }, () => {
            });

            return _t.Task;
        }

        private Scene()
        {
        }

        private Task saveScene()
        {
            var _t = new TaskCompletionSource();

            var query = new DBQueryHelper();
            query.condition("DataType", "PlayerList");
            query.condition("SceneName", sceneName);
            query.condition("SceneUUID", sceneUUID);
            var data = new UpdateDataHelper();
            data.set(players.Keys);
            dbProxy.getCollection("aiStarve", "game").updataPersistedObject(query.query(), data.data(), true, (ret) => {
                Log.Log.info($"Scene saveScene updataPersistedObject result:{ret}");
            });

            return _t.Task;
        }

        public static async Task<Scene> LoadScene(string _sceneName, string _sceneUUID)
        {
            var scene = new Scene();
            scene.dbProxy = Hub.Hub.get_random_dbproxyproxy();
            scene.sceneName = _sceneName;
            scene.sceneUUID = _sceneUUID;

            var playerList = await scene.loadPlayers();
            if (playerList != null)
            {
                foreach (var playerGuid in playerList)
                {
                    var player = await Player.LoadPlayer(scene.dbProxy, scene.sceneName, playerGuid);
                    if (player != null)
                    {
                        scene.Players.Add(playerGuid, player);
                    }
                }
            }

            return scene;
        }

        public static async Task<Scene> CreateScene(string _sceneName)
        {
            var scene = new Scene();
            scene.dbProxy = Hub.Hub.get_random_dbproxyproxy();
            scene.sceneName = _sceneName;
            scene.sceneUUID = Guid.NewGuid().ToString();

            await scene.saveScene();

            return scene;
        }

        private SceneInfo sceneInfo()
        {
            var info = new SceneInfo();
            info.users = new List<UserInformation>();
            info.fences = new List<Fence>();
            info.buildings = new List<Building>();

            foreach(var player in players.Values)
            {
                if (player.IsOnline)
                {
                    info.users.Add(player.userInformation);
                } 

                info.fences.AddRange(player.fences);
                info.buildings.AddRange(player.buildings);
            }

            return info;
        }

        public bool CheckInFence(Pos pos)
        {
            foreach (var player in players.Values)
            {
                if (player.CheckInFence(pos))
                {
                    return true;
                }
            }
            return false;
        }

        public void PlayerIntoScene(string userGuid, string userName, string userNetUUID)
        {
            var player = players.GetValueOrDefault(userGuid, null);
            if (player == null)
            {
                player = new Player();
                player.userInformation = new UserInformation();
                player.userInformation.UserName = userName;
                player.userInformation.UserGuid = userGuid;

                do
                {
                    var y = RandomHelper.RandomInt(Length);
                    var x = RandomHelper.RandomInt(Width);
                    player.userInformation.Pos.X = x;
                    player.userInformation.Pos.Y = y;
                } while (!CheckInFence(player.userInformation.Pos));

                players.Add(userGuid, player);
                netUUIDPlayers.Add(userNetUUID, player);
            }
            player.NetUUID = userNetUUID;

            SceneClientCaller.get_multicast(players.Keys.ToList()).scene_info(sceneInfo());
        }

        public void PlayerLevelScene(string userNetUUID)
        {
            var player = netUUIDPlayers.GetValueOrDefault(userNetUUID, null);
            if (player != null)
            {
                player.NetUUID = string.Empty;
                player.userInformation.dir = Direction.None;

                SceneClientCaller.get_multicast(players.Keys.ToList()).scene_info(sceneInfo());
            }
        }
    }

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

        public Scene GetScene(string sceneUUID)
        {
            return scenes.GetValueOrDefault(sceneUUID, null);
        }

        public void PlayerIntoScene(string sceneUUID, string userGuid, string userName, string userNetUUID)
        {
            if (scenes.TryGetValue(sceneUUID, out var scene))
            {
                scene.PlayerIntoScene(userGuid, userName, userNetUUID);
            }
        }

        public void ClientDisconnect(string uuid)
        {
            if (clientScenes.TryGetValue(uuid, out var scene))
            {
                scene.PlayerLevelScene(uuid);
            }
        }
    }
}