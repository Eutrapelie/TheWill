using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace TheWill
{
    [Category("♥ The Will")]
    public class AddCodeLine : ActionTask
    {
        public BBParameter<List<CodeLine>> playerList;
        public CodeLine codeLine;

        protected override string info
        {
            get
            {
                return string.Format("Add CodeLine {0} to player", codeLine);
            }
        }

        protected override void OnExecute()
        {
            if (playerList.value.Contains(codeLine))
            {
                Debug.LogError("[AddCodeLine] Adding code line to Player that it already had : " + codeLine.ToString());
            }
            else
            {
                Debug.Log("<color=blue>[AddCodeLine] Add code line to Player: " + codeLine.ToString() + "</color>");
                for (int i = 0; i < playerList.value.Count; i++)
                    Debug.Log("<color=blue>" + playerList.value[i].ToString() + "</color>");
                playerList.value.Add(codeLine);
            }
            EndAction(true);
        }
    }
}
