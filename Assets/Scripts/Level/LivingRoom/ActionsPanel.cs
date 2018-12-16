using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

namespace TheWill
{
    public enum BackpackElement { GoToHall, Book, Explore}

    [RequireComponent(typeof(Animator))]
    public class ActionsPanel : MonoBehaviour, IPointerExitHandler
    {
        static ActionsPanel _instance;
        public static ActionsPanel Instance { get { return _instance; } }

        Animator _animator;
        [Header("Backpack")]
        [SerializeField] Button _goToHallButton;
        [SerializeField] Button _bookButton;
        [SerializeField] Button _exploreButton;
        [SerializeField] Backpack _backpack;
        [SerializeField] BackpackPanel _backpackPanel;

        [Header("Change of room")]
        [SerializeField] Canvas _fadeCanvas;
        [SerializeField] AnimationClip _closingAnim;
        
        [Tooltip("Let 'None' if the current room isn't 'Hall'")]
        [SerializeField] RoomsChoice _roomsChoice;

        [Header("Explore")]
        [SerializeField] CanvasGroup _vnDialogCanvasGroup;
        [SerializeField] ChangeSpriteHierarchyVisibility _spriteHierarchyVisibility;
        [SerializeField] Animator _exploreAnimator;
        bool _isExploreMode;

        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _instance = this;
            _animator = GetComponent<Animator>();
            Debug.Assert(_goToHallButton != null, "_goToHallButton cannot be null.");
            Debug.Assert(_bookButton != null, "_bookButton cannot be null.");
            Debug.Assert(_exploreButton != null, "_exploreButton cannot be null.");
            Debug.Assert(_backpack != null, "_backpack cannot be null.");
            Debug.Assert(_backpackPanel != null, "_backpackPanel cannot be null.");

            Debug.Assert(_fadeCanvas != null, "_fadeCanvas cannot be null.");
            Debug.Assert(_closingAnim != null, "_closingAnim cannot be null.");

            if (SceneManager.GetActiveScene().name == "Hall")
                Debug.Assert(_roomsChoice != null, "_roomsChoice cannot be null.");

            Debug.Assert(_vnDialogCanvasGroup != null, "_vnDialogCanvasGroup cannot be null.");
            Debug.Assert(_spriteHierarchyVisibility != null, "_spriteHierarchyVisibility cannot be null.");
            Debug.Assert(_exploreAnimator != null, "_exploreAnimator cannot be null.");
        }
        /*********************************************************/

        void Start()
        {
            _isExploreMode = false;
            _backpack.OnMouseEnter += OnMouseEnterBackpack;
            _goToHallButton.interactable = Game.Current.backpackElement > 0;
            _bookButton.interactable = Game.Current.backpackElement > 1;
            _exploreButton.interactable = Game.Current.backpackElement > 2;
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void OnMouseEnterBackpack()
        {
            _animator.SetBool("Show", true);
        }
        /*********************************************************/

        IEnumerator LaunchClosingAndChangeRoomCoroutine(string a_roomName)
        {
            yield return new WaitForSeconds(_closingAnim.length);
            SceneManager.LoadScene(a_roomName);
            SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);

            switch (a_roomName)
            {
                case "Hall":
                    Game.Current.currentRoom = Room.Hall;
                    break;
            }
            /*if ((Room)a_roomName != Room.None)
            {
                Destroy(gameObject);
                Game.Current.currentRoom = newRoom;
            }*/
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void Btn_OpenBackpack()
        {
            _backpackPanel.SetVisibility(true);
        }
        /*********************************************************/

        public void Btn_SetExploreMode(bool a_show)
        {
            _isExploreMode = a_show;
            _vnDialogCanvasGroup.alpha = _isExploreMode ? 0 : 1;
            _vnDialogCanvasGroup.blocksRaycasts = _isExploreMode;
            _vnDialogCanvasGroup.interactable = _isExploreMode;
            _spriteHierarchyVisibility.SetSpritesVisibility(!_isExploreMode);
            _exploreAnimator.SetBool("Show", _isExploreMode);
            _animator.SetBool("HideActions", _isExploreMode);
        }
        /*********************************************************/

        public void Btn_ChangeRoom(string a_roomName)
        {
            _fadeCanvas.GetComponent<Animator>().SetTrigger("Closing");
            StartCoroutine(LaunchClosingAndChangeRoomCoroutine(a_roomName));
        }
        /*********************************************************/

        public void Btn_OpenRoomsChoice()
        {
            _roomsChoice.SetVisibility(true);
        }
        /*********************************************************/

        public void ActivateBackpackElement(BackpackElement a_element)
        {
            switch (a_element)
            {
                case BackpackElement.GoToHall:
                    _goToHallButton.interactable = true;
                    Game.Current.backpackElement = 1;
                    break;
                case BackpackElement.Book:
                    _bookButton.interactable = true;
                    Game.Current.backpackElement = 2;
                    break;
                case BackpackElement.Explore:
                    _exploreButton.interactable = true;
                    Game.Current.backpackElement = 3;
                    break;
            }
            _backpack.GetComponent<Animator>().enabled = true;
        }
        /*********************************************************/

        #region Interface
        public void OnPointerExit(PointerEventData a_eventData)
        {
            _animator.SetBool("Show", false);
        }
        /*********************************************************/
        #endregion
    } // class
} // namespace
