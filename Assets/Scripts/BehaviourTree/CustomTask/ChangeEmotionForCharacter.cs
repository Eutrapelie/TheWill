using NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;

public class ChangeEmotionForCharacter : ActionTask
{
    public static string EVT_CHARACTER_CHANGE_EMOTION = "ChangeEmotionForCharacter.EVT_CHARACTER_CHANGE_EMOTION";
    public Character character;
    public Emotion newEmotion;

    protected override string info
    {
        get
        {
            return string.Format("{0} is now feeling {1}", character, newEmotion);
        }
    }

    protected override void OnExecute()
    {
        List<object> args = new List<object>();
        args.Add(character);
        args.Add(newEmotion);

        EventManager.TriggerEvent(EVT_CHARACTER_CHANGE_EMOTION, args);        
        EndAction(true);
    }
}
