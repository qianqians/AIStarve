
using System.Reflection.Metadata;

namespace Abelkhan
{
    public class RedisHelp
    {
        public static string BuildLoadSceneLockKey()
        {
            return $"Server:LoadSceneLockKey";
        }

        public static string BuildSceneInfoListKey()
        {
            return $"Server:SceneInfoListKey";
        }

        public static int SceneInfoListTimeout = 24 * 60 * 60 * 1000;
    }
}
