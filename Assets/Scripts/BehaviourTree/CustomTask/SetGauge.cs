using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;

public class SetGauge : ActionTask
{
    public static string EVT_CHARACTER_SET_GAUGE = "SetGauge.EVT_CHARACTER_SET_GAUGE";
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
        List<object> args = new List<object>();
        args.Add(character);
        args.Add(value);

        EventManager.TriggerEvent(EVT_CHARACTER_SET_GAUGE, args);
        EndAction(true);
    }
}
