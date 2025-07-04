using Abelkhan;
using InfluxData.Net.InfluxDb.Models.Responses;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
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
        private UserInformation userInformation = null;
        private List<Fence> fences = [];
        private List<Building> buildings = [];

        private bool isOnline = false;
        public bool IsOnline
        {
            get { 
                return isOnline; 
            } 
        
            set {
                isOnline = value;
            }
        }

        private Hub.DBProxyProxy _dbProxy;

        static private Task<UserInformation> loadUserInformation(Hub.DBProxyProxy _dbProxy, string sceneName, long userGuid)
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

        private Task loadFence(string sceneName, long userGuid)
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

        private Task loadBuilding(string sceneName, long userGuid)
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

        public static async Task<Player> LoadPlayer(Hub.DBProxyProxy _dbProxy, string sceneName, long userGuid)
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

        private Dictionary<long, Player> players = new();
        public Dictionary<long, Player> Players { 
            get { 
                return players; 
            } 
        }

        private Hub.DBProxyProxy dbProxy;
        private string sceneName;
        private string sceneUUID;

        private Task<List<long>> loadPlayers()
        {
            var _t = new TaskCompletionSource<List<long>>();

            List<long> list = null;
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
                    list = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<List<long>>(doc as BsonDocument);
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
                    scene.Players.Add(playerGuid, player);
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
    }
}