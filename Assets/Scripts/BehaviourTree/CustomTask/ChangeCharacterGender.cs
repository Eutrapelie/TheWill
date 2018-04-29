using System.Collections;
using NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [Category("The Will")]
    public class ChangeCharacterGender : ActionTask
    {
        public Genre genre;


        protected override string info
        {
            get
            {
                return string.Format("Kim is now a {0}", genre);
            }
        }

        protected override void OnExecute()
        {
            Debug.Log("[ChangeCharacterGender] " + genre);
            Game.Current.player.genre = genre;
            GameManager.Instance.PlayerController.PlayerCard.Player.genre = genre;
            EndAction(true);
        }
    }
}
