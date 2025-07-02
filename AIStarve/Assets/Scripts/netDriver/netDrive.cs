using UnityEngine;

public class netDriver : MonoBehaviour
{
    private Client.Client _client;

    private static Abelkhan.login_caller _login_caller;
    public static Abelkhan.login_caller LoginCaller
    {
        get
        {
            return _login_caller;
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

    private Abelkhan.scene_client_module _scene_client_Module;

    void Start()
    {
        DontDestroyOnLoad(this);

        _client = new Client.Client();
        _login_caller = new Abelkhan.login_caller(_client);
        _scene_caller = new Abelkhan.scene_caller(_client);

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
