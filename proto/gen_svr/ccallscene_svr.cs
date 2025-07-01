using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace Abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

/*this struct code is codegen by abelkhan codegen for c#*/
/*this module code is codegen by abelkhan codegen for c#*/
    public class scene_module : Common.IModule {
        public scene_module()
        {
            Hub.Hub._modules.add_mothed("scene_move", move);
            Hub.Hub._modules.add_mothed("scene_building", building);
        }

        public event Action<Pos, Direction> on_move;
        public void move(IList<MsgPack.MessagePackObject> inArray){
            var _start = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)inArray[0]).AsDictionary());
            var _dir = (Direction)((MsgPack.MessagePackObject)inArray[1]).AsInt32();
            if (on_move != null){
                on_move(_start, _dir);
            }
        }

        public event Action<List<Building>> on_building;
        public void building(IList<MsgPack.MessagePackObject> inArray){
            var _buildings = new List<Building>();
            var _protocol_arraybuildings = ((MsgPack.MessagePackObject)inArray[0]).AsList();
            foreach (var v_4435b032_5f03_5920_bcc2_f3356d041cc1 in _protocol_arraybuildings){
                _buildings.Add(Building.protcol_to_Building(((MsgPack.MessagePackObject)v_4435b032_5f03_5920_bcc2_f3356d041cc1).AsDictionary()));
            }
            if (on_building != null){
                on_building(_buildings);
            }
        }

    }

}
