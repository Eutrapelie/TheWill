using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace TheWill
{
    [Category("♥ The Will")]
    public class SwitchExploreMode : ActionTask
    {
        public BBParameter<bool> switchToExploreMode;

        protected override string info
        {
            get
            {
                return string.Format(switchToExploreMode.value ? "Switch between classic and explore mode" : "Switch between explore and classic mode");
            }
        }
        

        protected override void OnExecute()
        {
            Debug.Log("<color=orange>[SwitchToFromItemBT] " + (switchToExploreMode.value ? "Switch between classic and explore mode" : "Switch between explore and classic mode" + "</color>"));
            EventManager.TriggerEvent(ActionsPanel.EVT_SWITCH_MODE, switchToExploreMode.value);
            EndAction(true);
        }
        /*********************************************************/
    }
}
