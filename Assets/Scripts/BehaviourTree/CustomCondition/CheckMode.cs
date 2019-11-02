using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace TheWill
{
    [Category("♥ The Will")]
    public class CheckMode : ConditionTask
    {
        public BBParameter<ActionMode> mode;

        protected override string info
        {
            get { return "Test pour vérifier que le mode en cours est bien " + mode; }
        }


        protected override bool OnCheck()
        {
            if (ActionsPanel.Instance.currentMode == mode.value)
            {
                Debug.Log("<color=blue>Current mode is " + mode + "</color>");
                return true;
            }

            return false;
        }
        /*********************************************************/
    }
}