using System.Collections.Generic;
using System.Threading.Tasks;
using Abelkhan;
using UnityEngine;

public class netScene
{
    public static void RemoveBuilding(List<Building> buildings, List<Fence> fences)
    {
        netDriver.SceneCaller.get_hub(netDriver.SceneHubInfo.scene_hub_id).remove_building(buildings, fences);
    }
    public static void Building(List<Building> buildings, List<Fence> fences)
    {
        netDriver.SceneCaller.get_hub(netDriver.SceneHubInfo.scene_hub_id).building(buildings, fences);
    }

    public static void Move(Pos start, Direction dir)
    {
        netDriver.SceneCaller.get_hub(netDriver.SceneHubInfo.scene_hub_id).move(start, dir);
    }
}
