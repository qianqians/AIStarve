using System.Collections.Generic;
using System.Threading.Tasks;
using Abelkhan;

public class netLogin
{
    public static async Task<List<scene_hub_info>> Login(string account)
    {
        var _t = new TaskCompletionSource<List<scene_hub_info>>();

        var info = await netDriver.GetHubInfo("server");
        netDriver.LoginCaller.get_hub(netDriver.ServerHubName).player_login_no_token(account).callBack((list) =>
        {
            _t.SetResult(list);
        }, (err) =>
        {
            Log.Log.err("player_login_no_token err:{0}", err);
            _t.SetResult(null);
        }).timeout(3000, () =>
        {
            Log.Log.err("player_login_no_token timeout");
            _t.SetResult(null);
        });

        return await _t.Task;
    }
}
