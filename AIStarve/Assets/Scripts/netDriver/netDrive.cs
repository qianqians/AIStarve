using System.Threading.Tasks;
using Abelkhan;
using UnityEngine;

public class netDriver : MonoBehaviour
{
    public static string ServerHubName;
    public static scene_hub_info SceneHubInfo;

    private static Client.Client _client;

    private static Abelkhan.login_caller _login_caller;
    public static Abelkhan.login_caller LoginCaller
    {
        get
        {
            return _login_caller;
        }
    }

    private static Abelkhan.scene_mgr_caller _scene_mgr_caller;
    public static Abelkhan.scene_mgr_caller SceneMgrCaller
    {
        get
        {
            return _scene_mgr_caller;
        }
    }

    private static Abelkhan.scene_caller _scene_caller;
    public static Abelkhan.scene_caller SceneCaller
    {
        get
        {
            return _scene_caller;
        }
    }

    private static Abelkhan.scene_client_module _scene_client_Module;
    public static Abelkhan.scene_client_module SceneNtfModule
    {
        get
        {
            return _scene_client_Module;
        }
    }

    public static Task<Abelkhan.hub_info> GetHubInfo(string hubType)
    {
        var _t = new TaskCompletionSource<Abelkhan.hub_info>();

        _client.get_hub_info(hubType, (_info) =>
        {
            ServerHubName = _info.hub_name;
            _t.SetResult(_info);
        });

        return _t.Task;
    }

    void Start()
    {
        DontDestroyOnLoad(this);

        _client = new Client.Client();
        _login_caller = new Abelkhan.login_caller(_client);
        _scene_caller = new Abelkhan.scene_caller(_client);
        _scene_mgr_caller = new scene_mgr_caller(_client);

        _scene_client_Module = new Abelkhan.scene_client_module(_client);

        _client.connect_gate("ws://127.0.0.1:3001", 3000);
        _client.onGateConnect += () => {

        };
        _client.onGateConnectFaild += () => {
            Debug.Log("connect gate faild!");
        };
        _client.onHubConnect += (hub_name) => {
            Debug.Log(string.Format("connect hub:{0} sucessed!", hub_name));
        };
        _client.onHubConnectFaild += (hub_name) => {
            Debug.Log(string.Format("connect hub:{0} faild!", hub_name));
        };

    }

    // Update is called once per frame
    void Update()
    {
        _client.poll();
    }
}
