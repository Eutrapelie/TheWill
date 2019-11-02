using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;

namespace TheWill
{
    [Category("TheWill")]
    public class CheckItemIsSelected : ConditionTask
    {
        public BBParameter<ItemCard> itemCard;


        protected override string info
        {
            get { return "Test pour checker si l'item est sélectionné"; }
        }

        protected override bool OnCheck()
        {
            if (itemCard.value.isClicked)
            {
                Debug.Log("<color=blue>Hey!</color>");
                itemCard.value.isClicked = false;
                return true;
            }

            return false;
        }
    }
}