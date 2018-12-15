using System.Collections;
using NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [Category("♥ The Will")]
    public class ChangeDay : ActionTask
    {
        public int day;


        protected override string info
        {
            get
            {
                return string.Format("Change to day {0}", day);
            }
        }

        protected override void OnExecute()
        {
            Debug.Log("[ChangeDay] " + day);
            Game.Current.dayNumber = day;
            EndAction(true);
        }
    }
}
