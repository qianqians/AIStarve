using System.Collections.Generic;
using System.Threading.Tasks;
using Abelkhan;
using UnityEngine;

public class netSceneMgr
{
    public static Task<List<scene_hub_info>> GetSceneList()
    {
        var _t = new TaskCompletionSource<List<scene_hub_info>>();

        netDriver.SceneMgrCaller.get_hub(netDriver.ServerHubName).get_scene_list().callBack((list) =>
        {
            _t.SetResult(list);
        }, (err) =>
        {
            Log.Log.err("get_scene_list err:{0}", err);
            _t.SetResult(null);
        }).timeout(3000, () =>
        {
            Log.Log.err("get_scene_list timeout");
            _t.SetResult(null);
        });

        return _t.Task;
    }

    public static Task<scene_hub_info> CreateScene(string sceneName)
    {
        var _t = new TaskCompletionSource<scene_hub_info>();

        netDriver.SceneMgrCaller.get_hub(netDriver.ServerHubName).create_scene(sceneName).callBack((info) =>
        {
            _t.SetResult(info);
        }, (err) =>
        {
            Log.Log.err("create_scene failed:{0}", err);
            _t.SetResult(null);
        }).timeout(3000, () =>
        {
            Log.Log.err("create_scene timeout");
            _t.SetResult(null);
        });

        return _t.Task;
    }

    public static Task<bool> LeaveScene()
    {
        var _t = new TaskCompletionSource<bool>();

        netDriver.SceneMgrCaller.get_hub(netDriver.ServerHubName).player_level_scene(netDriver.SceneHubInfo.scene_guid).callBack(() =>
        {
            _t.SetResult(true);
        }, (err) =>
        {
            Log.Log.err("player_level_scene failed:{0}", err);
            _t.SetResult(false);
        }).timeout(3000, () =>
        {
            Log.Log.err("player_level_scene timeout");
            _t.SetResult(false);
        });
        netDriver.SceneHubInfo = null;

        return _t.Task;
    }

    public static Task<SceneInfo> IntoScene(string playerGuid, string playerName, scene_hub_info sceneHubInfo)
    {
        var _t = new TaskCompletionSource<SceneInfo>();

        var serverName = sceneHubInfo.scene_name;
        if (string.IsNullOrEmpty(serverName))
        {
            serverName = netDriver.ServerHubName;
        }
        netDriver.SceneMgrCaller.get_hub(serverName).player_into_scene(
            sceneHubInfo.scene_guid, sceneHubInfo.scene_name, playerGuid, playerName).callBack((sceneInfo) =>
            {
                netDriver.SceneHubInfo = sceneHubInfo;
                netDriver.ServerHubName = sceneHubInfo.scene_hub_id;
                _t.SetResult(sceneInfo);
            }, async (err, sceneHubInfo) =>
            {
                if (err == (int)em_error.scene_in_other_server)
                {
                    _t.SetResult(await IntoScene(playerGuid, playerName, sceneHubInfo));
                }
                else
                {
                    Log.Log.err("player_into_scene failed:{0}", err);
                    _t.SetResult(null);
                }
            }).timeout(3000, () =>
            {
                Log.Log.err("player_into_scene timeout");
                _t.SetResult(null);
            });

        return _t.Task;
    }
}
