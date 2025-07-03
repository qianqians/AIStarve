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
                    _t.SetResult();
                }
                else
                {
                    _t.SetResult();
                }
            }, () => { });

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
                    _t.SetResult();
                }
                else
                {
                    _t.SetResult();
                }
            }, () => { });

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
    }

    public class Scene
    {
        private Hub.DBProxyProxy _dbProxy;
    }
}