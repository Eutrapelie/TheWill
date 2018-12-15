using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [Category("♥ The Will")]
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
}
