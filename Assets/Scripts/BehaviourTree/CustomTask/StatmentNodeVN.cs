using UnityEngine;
using NodeCanvas.DialogueTrees;
using ParadoxNotion.Design;
using NodeCanvas;
using NodeCanvas.Framework;
using System;
using System.Linq;

namespace VN.Dialog
{
    [Name("SayVN")]
    [Description("DIALOG VN : Make the selected Dialogue Actor talk. You can make the text more dynamic by using variable names in square brackets\ne.g. [myVarName] or [Global/myVarName]")]
    public class StatmentNodeVN : StatementNode
    {
        public static string EVT_CHARACTER_TALKING = "StatmentNodeVN.EVT_CHARACTER_TALKING";
        protected override Status OnExecute(Component agent, IBlackboard bb)
        {
            MyCharacterController mCC = GameManager.Instance.MyCharacterController;
            if (mCC != null)
            {
                Character? charEnum = null;
                try
                {
                    charEnum = ((Character)Enum.Parse(typeof(Character), actorName));
                }
                catch (ArgumentException)
                {
                    Debug.Log(actorName + " is not a member of the Character enumeration.");
                }
                if(charEnum != null)                    
                    EventManager.TriggerEvent(EVT_CHARACTER_TALKING, charEnum.Value);
            }
            statement = GenreReplace(statement);

            return base.OnExecute(agent, bb);
        }

        private Statement GenreReplace(Statement text)
        {
            var s = text.text;
            var i = 0;
            while ((i = s.IndexOf('#', i)) != -1)
            {

                var end = s.Substring(i + 1).IndexOf('#');
                var input = s.Substring(i + 1, end); //what's in the #
                var output = s.Substring(i, end + 2); //what should be replaced (includes brackets)

                string o = "";
                string[] genres = input.Split('|');
                if(GameManager.Instance.PlayerController.PlayerCard.Player.genre.Equals(Genre.Woman))
                {
                    o = genres[0];
                }
                else if(GameManager.Instance.PlayerController.PlayerCard.Player.genre.Equals(Genre.Man))
                {
                    o = genres[1];
                }
                else
                {
                    o = genres[2];
                }
               

                s = s.Replace(output, o != null ? o.ToString() : output);

                i++;
            }

            return new Statement(s, text.audio, text.meta);
        }

    }
}
