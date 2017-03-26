using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class SetGauge : ActionTask
{
    public Gauge gauge;
    public int value = 0;
    public Character character;

    protected override string info
    {
        get
        {
            return string.Format(" Change gauge value of {0} to character {1} by {2} points", value, character, value);
        }
    }

    protected override void OnExecute()
    {
        GameManager.Instance.MyCharacterController.characterSetGaugeEvent.Invoke(character, value);
        EndAction(true);
    }
}
