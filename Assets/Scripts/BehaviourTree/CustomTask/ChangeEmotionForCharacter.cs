using NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class ChangeEmotionForCharacter : ActionTask
{
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
        GameManager.Instance.MyCharacterController.characterChangeEmotionEvent.Invoke(character, newEmotion);
        EndAction(true);
    }
}
