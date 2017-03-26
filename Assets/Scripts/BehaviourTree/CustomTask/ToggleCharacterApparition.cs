using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCharacterApparition : ActionTask
{
    public Character character;
    public bool isHere;

    protected override string info
    {
        get
        {
            if(isHere)
            {
                return string.Format("Character {0} is appearing !", character);
            }
            return string.Format("Character {0} is disappearing !", character);
        }
    }

    protected override void OnExecute()
    {
        GameManager.Instance.MyCharacterController.characterTogglingEvent.Invoke(character, isHere);
        EndAction(true);
    }
}
