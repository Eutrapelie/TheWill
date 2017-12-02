using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

namespace TheWill
{
    [RequireComponent(typeof(Animator))]
    public class ActionsPanel : MonoBehaviour, IPointerExitHandler
    {
        Animator _animator;
        [Header("Backpack")]
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
            _animator = GetComponent<Animator>();
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
        }
        /*********************************************************/

        void Update() { }
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

        #region Interface
        public void OnPointerExit(PointerEventData a_eventData)
        {
            _animator.SetBool("Show", false);
        }
        /*********************************************************/
        #endregion
    } // class
} // namespace
