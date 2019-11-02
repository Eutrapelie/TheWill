using NodeCanvas.Framework;
using NodeCanvas.Tasks.Conditions;
using ParadoxNotion;
using ParadoxNotion.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace TheWill
{
    [Description("For click on character :\nWill subscribe to a public event of Action<Character> type and return true when the event is raised.")]
    [EventReceiver("OnCustomEvent")]
    public class CheckCharacterOnClickEventNew : ConditionTask<GraphOwner>
    {
        public BBParameter<string> eventName;
        public BBParameter<CharacterCard> checkValue;
    

        protected override string info
        {
            get
            {
                if (string.IsNullOrEmpty(eventName.value))
                    return "No Event Selected";
                return string.Format("'{0}' Raised", eventName);
            }
        }
        
        protected override bool OnCheck() { return false; }

        public void OnCustomEvent(EventData a_receivedEvent)
        {
            Debug.Log(eventName + " (" + a_receivedEvent  + ") isActive :" + isActive);
            if (isActive && a_receivedEvent.name.ToUpper() == eventName.value.ToUpper())
            {
                if (a_receivedEvent.value is CharacterCard)
                {
                    Debug.Log("Bonne piste");

                    if ((CharacterCard)a_receivedEvent.value == checkValue.value)
                    {
                        Debug.Log("Yeeeeeeeeeeep ! Value: " + checkValue.value.name);
#if UNITY_EDITOR
                        if (NodeCanvas.Editor.NCPrefs.logEvents)
                        {
                            Debug.Log(string.Format("Event '{0}' Received from '{1}'", a_receivedEvent.name, agent.gameObject.name), agent);
                        }
#endif

                        YieldReturn(true);
                    }
                }
            }
        }        
    }
}
