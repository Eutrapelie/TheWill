using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomForCharacter : ActionTask
{
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
        GameManager.Instance.MyCharacterController.characterChangeRoomEvent.Invoke(character, newRoom);
        GameManager.Instance.MyCharacterController.characterChangeRoomSpotEvent.Invoke(character, newSpot);
        EndAction(true);
    }
}
