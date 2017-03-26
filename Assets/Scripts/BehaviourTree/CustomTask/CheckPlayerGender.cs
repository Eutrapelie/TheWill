using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParadoxNotion.Design;

public class CheckPlayerGender : ConditionTask
{
    public Genre gender;
    public BBParameter<PlayerCard> playerCard;

    protected override string info
    {
        get
        {
            return string.Format("Is Player {0} ?", gender.ToString());
        }
    }

    protected override bool OnCheck()
    {
        if (gender.Equals(playerCard.value.Player.genre))
        {
            return true;
        }
        return false;
    }


}
