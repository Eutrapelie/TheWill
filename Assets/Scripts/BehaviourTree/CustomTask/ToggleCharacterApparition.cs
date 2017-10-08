using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCharacterApparition : ActionTask
{
    public static string EVT_TOGGLE_CHARACTER_APPARITION = "ToggleCharacterApparition.EVT_TOGGLE_CHARACTER_APPARITION";
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
        List<object> args = new List<object>();
        args.Add(character);
        args.Add(isHere);
        EventManager.TriggerEvent(EVT_TOGGLE_CHARACTER_APPARITION, args);
        EndAction(true);
    }
}
