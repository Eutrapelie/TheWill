using System.Collections;
using NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [Category("♥ The Will")]
    public class ActivateBackpackElement : ActionTask
    {
        public BackpackElement backpackElement;


        protected override string info
        {
            get
            {
                return string.Format("Activate {0} in backpack.", backpackElement);
            }
        }

        protected override void OnExecute()
        {
            Debug.Log("[ActivateBackpackElement] " + backpackElement);
            ActionsPanel.Instance.ActivateBackpackElement(backpackElement);
            EndAction(true);
        }
    }
}