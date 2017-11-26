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
        [SerializeField]
        Backpack _backpack;

        [SerializeField] Canvas _fadeCanvas;
        [SerializeField] AnimationClip _closingAnim;

        
        void Awake()
        {
            _animator = GetComponent<Animator>();

            Debug.Assert(_fadeCanvas != null, "_fadeCanvas cannot be null.");
            Debug.Assert(_closingAnim != null, "_closingAnim cannot be null.");
        }
        
        void Start()
        {
            _backpack.OnMouseEnter += OnMouseEnterBackpack;
        }
        
        void Update()
        {

        }

        void OnMouseEnterBackpack()
        {
            _animator.SetBool("Show", true);
        }

        IEnumerator LaunchClosingAndChangeRoomCoroutine(string a_roomName)
        {
            yield return new WaitForSeconds(_closingAnim.length);
            SceneManager.LoadScene(a_roomName);
        }

        public void Btn_ClickOnBackpack()
        {
            // Open BackpackPanel
        }

        public void Btn_ChangeRoom(string a_roomName)
        {
            _fadeCanvas.GetComponent<Animator>().SetTrigger("Closing");
            StartCoroutine(LaunchClosingAndChangeRoomCoroutine(a_roomName));
        }

        #region Interface
        public void OnPointerExit(PointerEventData a_eventData)
        {
            _animator.SetBool("Show", false);
        }
        #endregion
    } // class
} // namespace
