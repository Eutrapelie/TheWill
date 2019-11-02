using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace TheWill
{
    [Category("♥ The Will")]
    public class EnableCharacterInExploreMode : ActionTask
    {
        public BBParameter<CharacterCard> character;

        protected override string info
        {
            get
            {
                return string.Format("Enable character {0} during the exploration mode", character.value.CharacterInfo.characterName);
            }
        }
        

        protected override void OnExecute()
        {
            Debug.Log("<color=orange>[EnableCharacterInExploreMode] Enable " + character.value.CharacterInfo.characterName + " in exploration mode</color>");
            EventManager.TriggerEvent(MainController.EVT_UPSPOT_CHARACTER, character.value);
            EndAction(true);
        }
        /*********************************************************/
    }
}
