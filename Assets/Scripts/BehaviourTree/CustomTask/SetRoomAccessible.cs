using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;

namespace TheWill
{
    [Category("♥ The Will")]
    public class SetRoomAccessible : ActionTask
    {
        public BBParameter<Room> room;
        public bool isAccessible;

        protected override string info
        {
            get
            {
                if (isAccessible)
                    return string.Format("Set {0} to accessible in movement grid", room.value);
                else
                    return string.Format("Set {0} to not accessible in movement grid", room.value);
            }
        }
        

        protected override void OnExecute()
        {
            List<object> args = new List<object>();
            args.Add(room.value);
            args.Add(isAccessible);

            Debug.Log("<color=orange>[SetRoomAccessible] Set isAccessible of " + room.value + " to " + isAccessible + " in movement grid</color>");
            EventManager.TriggerEvent(RoomsChoice.EVT_ISACCESSIBLE_ROOM, args);
            EndAction(true);
        }
        /*********************************************************/
    }
}
