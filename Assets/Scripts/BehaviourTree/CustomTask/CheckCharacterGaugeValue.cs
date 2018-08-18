using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    public class CheckCharacterGaugeValue : ConditionTask
    {
        public Character character;
        public BBParameter<CharacterCard> characterCard;
        public int value;
        public bool equalTo;
        public bool greaterThan;
        public bool lessThan;

        protected override string info
        {
            get
            {
                string rep = string.Format("Is Character {0} gauge is ", character);

                if (equalTo && greaterThan)
                {
                    rep = rep + "greater or equal to";
                }
                else if (equalTo && lessThan)
                {
                    rep = rep + "less or equal to";
                }
                else if (equalTo)
                {
                    rep = rep + "equal to";
                }
                rep = rep + value.ToString() + " ?";

                return rep;

            }
        }

        protected override bool OnCheck()
        {
            if (equalTo && greaterThan)
            {
                if (characterCard.value.CharacterInfo.valueGauge >= value)
                    return true;
            }
            else if (equalTo && lessThan)
            {
                if (characterCard.value.CharacterInfo.valueGauge <= value)
                    return true;
            }
            else if (equalTo)
            {
                if (characterCard.value.CharacterInfo.valueGauge == value)
                    return true;
            }
            return false;
        }

    }
}
