using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomForCharacter : ActionTask
{
    public static string EVT_CHARACTER_CHANGE_ROOM = "ChangeRoomForCharacter.EVT_CHARACTER_CHANGE_ROOM";
    public static string EVT_CHARACTER_CHANGE_ROOM_SPOT = "ChangeRoomForCharacter.EVT_CHARACTER_CHANGE_ROOM_SPOT";
    public Character character;
    public Room newRoom;
    public RoomSpot newSpot;

    protected override string info
    {
        get
        {
            return string.Format("{0} is leaving the room to go {1} at spot {2}", character, newRoom.ToString(), newSpot.ToString());
        }
    }

    protected override void OnExecute()
    {
        List<object> argsRoom = new List<object>();
        List<object> argsRoomSpot = new List<object>();
        argsRoom.Add(character);
        argsRoom.Add(newRoom);
        argsRoomSpot.Add(character);
        argsRoomSpot.Add(newSpot);

        EventManager.TriggerEvent(EVT_CHARACTER_CHANGE_ROOM, argsRoom);
        EventManager.TriggerEvent(EVT_CHARACTER_CHANGE_ROOM_SPOT, argsRoomSpot);
        
        EndAction(true);
    }
}
