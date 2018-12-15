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

[Description("For click on character :\nWill subscribe to a public event of Action<Character> type and return true when the event is raised.")]
[EventReceiver("OnCustomEvent")]
public class CheckCharacterOnClickEvent<T> : ConditionTask<GraphOwner> {
    [SerializeField]
    private System.Type targetType = null;

    [SerializeField]
    private BBParameter<string> eventName = null;

    [SerializeField]
    private BBParameter<T> checkValue = null;
    
    /*public override Type agentType
    {
        get { return targetType ?? typeof(Transform); }
    }*/

    protected override string info
    {
        get
        {
            if (string.IsNullOrEmpty(eventName.value))
                return "No Event Selected";
            return string.Format("'{0}' Raised", eventName);
        }
    }


    protected override string OnInit()
    {        
        if (eventName == null)
            return "No Event Selected";

        var eventInfo = agentType.RTGetEvent(eventName.value);
        if (eventInfo == null)
        {
            return "Event was not found";
        }
        var methodInfo = this.GetType().RTGetMethod("Raised");
        var handler = methodInfo.RTCreateDelegate(eventInfo.EventHandlerType, this);
        eventInfo.AddEventHandler(agent, handler);
        return null;
    }

    public void Raised(T eventValue)
    {
        if (Equals(checkValue.value, eventValue))
        {
            YieldReturn(true);
        }
        else
        {
            YieldReturn(false);
        }
    }
   
    protected override bool OnCheck()
    {
        return false;
    }

    /*    ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
    #if UNITY_EDITOR

        protected override void OnTaskInspectorGUI()
        {

            if (!Application.isPlaying && GUILayout.Button("Select Event"))
            {
                Action<EventInfo> Selected = (e) => {
                    targetType = e.DeclaringType;
                    eventName = e.Name;
                };

                var menu = new UnityEditor.GenericMenu();
                if (agent != null)
                {
                    foreach (var comp in agent.GetComponents(typeof(Component)).Where(c => c.hideFlags == 0))
                    {
                        menu = EditorUtils.GetEventSelectionMenu(comp.GetType(), typeof(T), Selected, menu);
                    }
                    menu.AddSeparator("/");
                }
                foreach (var t in UserTypePrefs.GetPreferedTypesList(typeof(Component)))
                {
                    menu = EditorUtils.GetEventSelectionMenu(t, typeof(T), Selected, menu);
                }

                if (NodeCanvas.Editor.NCPrefs.useBrowser) { menu.ShowAsBrowser("Select Event", this.GetType()); }
                else { menu.ShowAsContext(); }
                Event.current.Use();
            }

            if (targetType != null)
            {
                GUILayout.BeginVertical("box");
                UnityEditor.EditorGUILayout.LabelField("Selected Type", agentType.FriendlyName());
                UnityEditor.EditorGUILayout.LabelField("Selected Event", eventName);
                GUILayout.EndVertical();

                EditorUtils.BBParameterField("Check Value", checkValue);
            }
        }

        #endif
    */
}
