using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLineNode : ActionTask
{
    public BBParameter<CodeLine> code;

    protected override string info
    {
        get
        {
            return string.Format("{0}", code);
        }
    }
}
