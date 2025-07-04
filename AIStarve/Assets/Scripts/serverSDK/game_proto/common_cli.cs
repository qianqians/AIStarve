using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MsgPack.Serialization;

namespace Abelkhan
{
/*this enum code is codegen by abelkhan codegen for c#*/

    public enum Direction{
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
/*this struct code is codegen by abelkhan codegen for c#*/
    public class Pos
    {
        public Int32 X;
        public Int32 Y;
        public static MsgPack.MessagePackObjectDictionary Pos_to_protcol(Pos _struct){
        if (_struct == null) {
            return null;
        }

            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("X", _struct.X);
            _protocol.Add("Y", _struct.Y);
            return _protocol;
        }
        public static Pos protcol_to_Pos(MsgPack.MessagePackObjectDictionary _protocol){
        if (_protocol == null) {
            return null;
        }

            var _struct29dd0b96_9335_398d_808e_0308d72c7d9a = new Pos();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "X"){
                    _struct29dd0b96_9335_398d_808e_0308d72c7d9a.X = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "Y"){
                    _struct29dd0b96_9335_398d_808e_0308d72c7d9a.Y = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _struct29dd0b96_9335_398d_808e_0308d72c7d9a;
        }
    }

    public class Building
    {
        public string UserGuid;
        public Int32 BuildingId;
        public string BuildingResource;
        public Pos topLeft;
        public Pos bottomRight;
        public static MsgPack.MessagePackObjectDictionary Building_to_protcol(Building _struct){
        if (_struct == null) {
            return null;
        }

            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("UserGuid", _struct.UserGuid);
            _protocol.Add("BuildingId", _struct.BuildingId);
            _protocol.Add("BuildingResource", _struct.BuildingResource);
            _protocol.Add("topLeft", new MsgPack.MessagePackObject(Pos.Pos_to_protcol(_struct.topLeft)));
            _protocol.Add("bottomRight", new MsgPack.MessagePackObject(Pos.Pos_to_protcol(_struct.bottomRight)));
            return _protocol;
        }
        public static Building protcol_to_Building(MsgPack.MessagePackObjectDictionary _protocol){
        if (_protocol == null) {
            return null;
        }

            var _structe37b319b_d83e_3aee_bc74_701941f721c3 = new Building();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "UserGuid"){
                    _structe37b319b_d83e_3aee_bc74_701941f721c3.UserGuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "BuildingId"){
                    _structe37b319b_d83e_3aee_bc74_701941f721c3.BuildingId = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "BuildingResource"){
                    _structe37b319b_d83e_3aee_bc74_701941f721c3.BuildingResource = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "topLeft"){
                    _structe37b319b_d83e_3aee_bc74_701941f721c3.topLeft = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "bottomRight"){
                    _structe37b319b_d83e_3aee_bc74_701941f721c3.bottomRight = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
            }
            return _structe37b319b_d83e_3aee_bc74_701941f721c3;
        }
    }

    public class Fence
    {
        public string UserGuid;
        public Int32 FenceId;
        public string FenceName;
        public Pos start;
        public Pos end;
        public static MsgPack.MessagePackObjectDictionary Fence_to_protcol(Fence _struct){
        if (_struct == null) {
            return null;
        }

            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("UserGuid", _struct.UserGuid);
            _protocol.Add("FenceId", _struct.FenceId);
            _protocol.Add("FenceName", _struct.FenceName);
            _protocol.Add("start", new MsgPack.MessagePackObject(Pos.Pos_to_protcol(_struct.start)));
            _protocol.Add("end", new MsgPack.MessagePackObject(Pos.Pos_to_protcol(_struct.end)));
            return _protocol;
        }
        public static Fence protcol_to_Fence(MsgPack.MessagePackObjectDictionary _protocol){
        if (_protocol == null) {
            return null;
        }

            var _struct6b7c8548_5d21_315c_a6ed_b41891d0e4d8 = new Fence();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "UserGuid"){
                    _struct6b7c8548_5d21_315c_a6ed_b41891d0e4d8.UserGuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "FenceId"){
                    _struct6b7c8548_5d21_315c_a6ed_b41891d0e4d8.FenceId = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "FenceName"){
                    _struct6b7c8548_5d21_315c_a6ed_b41891d0e4d8.FenceName = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "start"){
                    _struct6b7c8548_5d21_315c_a6ed_b41891d0e4d8.start = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "end"){
                    _struct6b7c8548_5d21_315c_a6ed_b41891d0e4d8.end = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
            }
            return _struct6b7c8548_5d21_315c_a6ed_b41891d0e4d8;
        }
    }

    public class UserInformation
    {
        public string sceneID;
        public string UserName;
        public string UserGuid;
        public string Avatar;
        public Int32 Gender;
        public Int32 Level;
        public Pos Pos;
        public Direction dir;
        public static MsgPack.MessagePackObjectDictionary UserInformation_to_protcol(UserInformation _struct){
        if (_struct == null) {
            return null;
        }

            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("sceneID", _struct.sceneID);
            _protocol.Add("UserName", _struct.UserName);
            _protocol.Add("UserGuid", _struct.UserGuid);
            _protocol.Add("Avatar", _struct.Avatar);
            _protocol.Add("Gender", _struct.Gender);
            _protocol.Add("Level", _struct.Level);
            _protocol.Add("Pos", new MsgPack.MessagePackObject(Pos.Pos_to_protcol(_struct.Pos)));
            _protocol.Add("dir", (Int32)_struct.dir);
            return _protocol;
        }
        public static UserInformation protcol_to_UserInformation(MsgPack.MessagePackObjectDictionary _protocol){
        if (_protocol == null) {
            return null;
        }

            var _struct07924b8f_25bc_32a4_b436_da6af6116572 = new UserInformation();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "sceneID"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.sceneID = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "UserName"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.UserName = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "UserGuid"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.UserGuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "Avatar"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.Avatar = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "Gender"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.Gender = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "Level"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.Level = ((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "Pos"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.Pos = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "dir"){
                    _struct07924b8f_25bc_32a4_b436_da6af6116572.dir = (Direction)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _struct07924b8f_25bc_32a4_b436_da6af6116572;
        }
    }

    public class SceneInfo
    {
        public List<Building> buildings;
        public List<Fence> fences;
        public List<UserInformation> users;
        public static MsgPack.MessagePackObjectDictionary SceneInfo_to_protcol(SceneInfo _struct){
        if (_struct == null) {
            return null;
        }

            var _protocol = new MsgPack.MessagePackObjectDictionary();
            if (_struct.buildings != null) {
                var _array_buildings = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.buildings){
                    _array_buildings.Add( new MsgPack.MessagePackObject(Building.Building_to_protcol(v_)));
                }
                _protocol.Add("buildings", new MsgPack.MessagePackObject(_array_buildings));
            }
            if (_struct.fences != null) {
                var _array_fences = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.fences){
                    _array_fences.Add( new MsgPack.MessagePackObject(Fence.Fence_to_protcol(v_)));
                }
                _protocol.Add("fences", new MsgPack.MessagePackObject(_array_fences));
            }
            if (_struct.users != null) {
                var _array_users = new List<MsgPack.MessagePackObject>();
                foreach(var v_ in _struct.users){
                    _array_users.Add( new MsgPack.MessagePackObject(UserInformation.UserInformation_to_protcol(v_)));
                }
                _protocol.Add("users", new MsgPack.MessagePackObject(_array_users));
            }
            return _protocol;
        }
        public static SceneInfo protcol_to_SceneInfo(MsgPack.MessagePackObjectDictionary _protocol){
        if (_protocol == null) {
            return null;
        }

            var _struct4f50ee72_55d3_3593_a651_f16393a46309 = new SceneInfo();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "buildings"){
                    _struct4f50ee72_55d3_3593_a651_f16393a46309.buildings = new List<Building>();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct4f50ee72_55d3_3593_a651_f16393a46309.buildings.Add(Building.protcol_to_Building(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "fences"){
                    _struct4f50ee72_55d3_3593_a651_f16393a46309.fences = new List<Fence>();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct4f50ee72_55d3_3593_a651_f16393a46309.fences.Add(Fence.protcol_to_Fence(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "users"){
                    _struct4f50ee72_55d3_3593_a651_f16393a46309.users = new List<UserInformation>();
                    var _protocol_array = ((MsgPack.MessagePackObject)i.Value).AsList();
                    foreach (var v_ in _protocol_array){
                        _struct4f50ee72_55d3_3593_a651_f16393a46309.users.Add(UserInformation.protcol_to_UserInformation(((MsgPack.MessagePackObject)v_).AsDictionary()));
                    }
                }
            }
            return _struct4f50ee72_55d3_3593_a651_f16393a46309;
        }
    }

    public class UserMoveInfo
    {
        public string UserGuid;
        public Pos Pos;
        public Direction dir;
        public static MsgPack.MessagePackObjectDictionary UserMoveInfo_to_protcol(UserMoveInfo _struct){
        if (_struct == null) {
            return null;
        }

            var _protocol = new MsgPack.MessagePackObjectDictionary();
            _protocol.Add("UserGuid", _struct.UserGuid);
            _protocol.Add("Pos", new MsgPack.MessagePackObject(Pos.Pos_to_protcol(_struct.Pos)));
            _protocol.Add("dir", (Int32)_struct.dir);
            return _protocol;
        }
        public static UserMoveInfo protcol_to_UserMoveInfo(MsgPack.MessagePackObjectDictionary _protocol){
        if (_protocol == null) {
            return null;
        }

            var _structde5d91e1_5db9_3a5e_985d_644be21eb795 = new UserMoveInfo();
            foreach (var i in _protocol){
                if (((MsgPack.MessagePackObject)i.Key).AsString() == "UserGuid"){
                    _structde5d91e1_5db9_3a5e_985d_644be21eb795.UserGuid = ((MsgPack.MessagePackObject)i.Value).AsString();
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "Pos"){
                    _structde5d91e1_5db9_3a5e_985d_644be21eb795.Pos = Pos.protcol_to_Pos(((MsgPack.MessagePackObject)i.Value).AsDictionary());
                }
                else if (((MsgPack.MessagePackObject)i.Key).AsString() == "dir"){
                    _structde5d91e1_5db9_3a5e_985d_644be21eb795.dir = (Direction)((MsgPack.MessagePackObject)i.Value).AsInt32();
                }
            }
            return _structde5d91e1_5db9_3a5e_985d_644be21eb795;
        }
    }

/*this module code is codegen by abelkhan codegen for c#*/

}
