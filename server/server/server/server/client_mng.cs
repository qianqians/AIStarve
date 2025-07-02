using Abelkhan;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server
{
    public class player_proxy
    {
        private readonly Hub.HubProxy _proxy;

        public string name
        {
            get
            {
                return _proxy.name;
            }
        }

    }
}