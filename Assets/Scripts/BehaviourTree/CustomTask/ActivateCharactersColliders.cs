using System.Collections;
using NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [Category("♥ The Will")]
    public class ActivateCharactersColliders : ActionTask
    {
        public bool enabled;


        protected override string info
        {
            get
            {
                if (enabled)
                    return string.Format("Set character's colliders enable.");
                else
                    return string.Format("Set character's colliders disable.");
            }
        }

        protected override void OnExecute()
        {
            Debug.Log("[ActivateCharactersColliders] " + enabled);
            MainController.Instance.ActivateAllCharactersColliders(enabled);
            EndAction(true);
        }
    }
}