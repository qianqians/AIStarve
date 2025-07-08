using Abelkhan;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
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
                    var player = await Player.LoadPlayer(scene.dbProxy, scene.sceneUUID, playerGuid);
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

        public SceneInfo SceneInfo()
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

        public List<UserMoveInfo> MoveInfo()
        {
            var list = new List<UserMoveInfo>();

            foreach (var p in players.Values)
            {
                if (!p.IsOnline)
                {
                    continue;
                }

                var info = new UserMoveInfo();
                info.Pos = p.userInformation.Pos;
                info.dir = p.userInformation.dir;
                list.Add(info);
            }

            return list;
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

            SceneClientCaller.get_multicast(players.Keys.ToList()).scene_info(SceneInfo());
        }

        public void PlayerLevelScene(string userNetUUID)
        {
            var player = netUUIDPlayers.GetValueOrDefault(userNetUUID, null);
            if (player != null)
            {
                player.NetUUID = string.Empty;
                player.userInformation.dir = Direction.None;

                SceneClientCaller.get_multicast(players.Keys.ToList()).scene_info(SceneInfo());
            }
        }

        public void PlayerMove(string userNetUUID, Pos pos, Direction dir)
        {
            var player = netUUIDPlayers.GetValueOrDefault(userNetUUID, null);
            if (player != null)
            {
                player.userInformation.Pos = pos;
                player.userInformation.dir = dir;

                SceneClientCaller.get_multicast(players.Keys.ToList()).move(MoveInfo());
            }
        }

        public void PlayerBuilding(string userNetUUID, List<Building> buildings, List<Fence> fences)
        {
            var player = netUUIDPlayers.GetValueOrDefault(userNetUUID, null);
            if (player != null)
            {
                player.buildings.AddRange(buildings);
                player.fences.AddRange(fences);

                SceneClientCaller.get_multicast(players.Keys.ToList()).scene_info(SceneInfo());
            }
        }
    }
}