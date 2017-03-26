using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class HasCodeLine : ConditionTask
{
    public BBParameter<List<CodeLine>> playerList;
    public CodeLine codeLine;

    protected override string info
    {
        get
        {
            
            string rep =  invert == false ? 
                        string.Format("CodeLine {0} \nexists", codeLine) 
                        : string.Format("CodeLine {0} \ndoesn't exist", codeLine);
            return rep;
        }
    }

    protected override bool OnCheck()
    {
        return playerList.value.Contains(codeLine);
    }
}
