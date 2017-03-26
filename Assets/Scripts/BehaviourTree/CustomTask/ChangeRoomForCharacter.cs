using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomForCharacter : ActionTask
{
    public Character character;
    public Room newRoom;

    protected override string info
    {
        get
        {
            return string.Format("{0} is leaving the room to go {1}", character, newRoom);
        }
    }

    protected override void OnExecute()
    {
        GameManager.Instance.MyCharacterController.characterChangeRoomEvent.Invoke(character, newRoom);
        EndAction(true);
    }
}
