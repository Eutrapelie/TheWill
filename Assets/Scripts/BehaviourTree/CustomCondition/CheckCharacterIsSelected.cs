using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;


namespace TheWill
{
    [Category("TheWill")]
    public class CheckCharacterIsSelected : ConditionTask
    {
        public BBParameter<CharacterCard> characterCard;



        protected override string info
        {
            get { return "Test pour checker si la carte est sélectionnée."; }
        }

        protected override bool OnCheck()
        {
            if (characterCard.value.isClicked)
            {
                Debug.Log("<color=blue>Hey!</color>");
                return true;
            }

            return false;
        }


    ////////////////////////////////////////
    ///////////GUI AND EDITOR STUFF/////////
    ////////////////////////////////////////
#if UNITY_EDITOR

        /*protected override void OnTaskInspectorGUI()
        {

            UnityEditor.EditorGUILayout.BeginHorizontal();
            pressType = (PressTypes)UnityEditor.EditorGUILayout.EnumPopup(pressType);
            key = (KeyCode)UnityEditor.EditorGUILayout.EnumPopup(key);
            UnityEditor.EditorGUILayout.EndHorizontal();
        }*/

#endif
    }
}