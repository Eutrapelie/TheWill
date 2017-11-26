using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomForKim : ActionTask
{
    public static string EVT_KIM_CHANGE_ROOM = "ChangeRoomForKim.EVT_KIM_CHANGE_ROOM";
    public Room newRoom;

    protected override string info
    {
        get
        {
            return string.Format("{Kim is leaving the room to go {0}", newRoom.ToString());
        }
    }

    protected override void OnExecute()
    {
        object argRoom = new object();
        argRoom = newRoom;

        EventManager.TriggerEvent(EVT_KIM_CHANGE_ROOM, argRoom);
        
        EndAction(true);
    }
}
