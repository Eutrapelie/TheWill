using NodeCanvas.DialogueTrees.UI.Examples;
using UnityEngine;

namespace TheWill
{
    [RequireComponent(typeof(Animator))]
    public class BackpackPanel : MonoBehaviour
    {
        Animator _animator;

        //1/DialogueUGUI _dialogueObject;
        DialogueUGUILocalization _dialogueObject;

        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void SetVisibility(bool a_show)
        {
            _animator.SetBool("Show", a_show);
            ActionsPanel.Instance.BlockInteractions(a_show);

            /*if (_dialogueObject == null)
                //1/_dialogueObject = FindObjectOfType<DialogueUGUI>();
                _dialogueObject = FindObjectOfType<DialogueUGUILocalization>();
            _dialogueObject.isGamePaused = a_show;
            GameManager.Instance.allowClickOnObject = !a_show;*/
        }
        /*********************************************************/
    }
}
