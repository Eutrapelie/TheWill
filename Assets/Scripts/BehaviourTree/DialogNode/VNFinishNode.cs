using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using NodeCanvas;

public class VNFinishNode : NodeCanvas.DialogueTrees.FinishNode
{
    public static string EVT_FINISH_DIALOG = "VNFinishNode.EVT_FINISH_DIALOG";

    public override string name
    {
        get { return "VNFINISH"; }
    }

    protected override Status OnExecute(Component agent, IBlackboard bb)
    {        
        EventManager.TriggerEvent(EVT_FINISH_DIALOG, new CharacterCard(null));

        return base.OnExecute(agent, bb);
    }
}
