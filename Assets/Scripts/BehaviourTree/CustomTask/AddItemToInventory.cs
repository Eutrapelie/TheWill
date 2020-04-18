using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace TheWill
{
    [Category("♥ The Will")]
    public class AddItemToInventory : ActionTask
    {
        public BBParameter<List<ItemCard>> playerItems;
        public ItemCard item;

        protected override string info
        {
            get
            {
                return string.Format("Add Item {0} to player", item);
            }
        }


        protected override void OnExecute()
        {
            if (playerItems.value.Contains(item))
            {
                Debug.LogError("[AddItemToInventory] Adding item to Player that it already had : " + item.ToString());
            }
            else
            {
                Debug.Log("<color=blue>[AddItemToInventory] Add item to Player: " + item.ToString() + "</color>");
                for (int i = 0; i < playerItems.value.Count; i++)
                    Debug.Log("<color=blue>" + playerItems.value[i].ToString() + "</color>");
                playerItems.value.Add(item);
            }
            EndAction(true);
        }
        /*********************************************************/
    }
}
