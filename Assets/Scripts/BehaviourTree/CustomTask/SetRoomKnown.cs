using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;

namespace TheWill
{
    [Category("♥ The Will")]
    public class SetRoomKnown : ActionTask
    {
        public BBParameter<Room> room;
        public bool isKnown;

        protected override string info
        {
            get
            {
                if (isKnown)
                    return string.Format("Set {0} to known in movement grid", room.value);
                else
                    return string.Format("Set {0} to unknown in movement grid", room.value);
            }
        }
        

        protected override void OnExecute()
        {
            List<object> args = new List<object>();
            args.Add(room.value);
            args.Add(isKnown);

            Debug.Log("<color=orange>[SetRoomKnown] Set isknown of " + room.value + " to " + isKnown + " in movement grid</color>");
            EventManager.TriggerEvent(RoomsChoice.EVT_ISKNOWN_ROOM, args);
            EndAction(true);
        }
        /*********************************************************/
    }
}
