using NodeCanvas.DialogueTrees.UI.Examples;
using UnityEngine;

namespace TheWill
{
    [RequireComponent(typeof(Animator))]
    public class BackpackPanel : MonoBehaviour
    {
        [SerializeField] RectTransform _inventoryObjectsParent;
        InventoryObject[] _inventoryObjects;
        Animator _animator;

        //1/DialogueUGUI _dialogueObject;
        DialogueUGUILocalization _dialogueObject;

        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _animator = GetComponent<Animator>();
            /*_inventoryObjects = new InventoryObject[_inventoryObjectsParent.childCount];
            for (int i = 0; i < _inventoryObjects.Length; i++)
                _inventoryObjects[i] = _inventoryObjectsParent.GetComponentsInChildren<InventoryObject>()[i];*/
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void SetVisibility(bool a_show)
        {
            _animator.SetBool("Show", a_show);
            ActionsPanel.Instance.BlockInteractions(a_show);
            if (a_show)
                Debug.Log("## " + GameManager.Instance.PlayerController.PlayerItems.Count);
        }
        /*********************************************************/
    }
}
