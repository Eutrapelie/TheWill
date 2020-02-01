using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheWill
{
    [RequireComponent(typeof(Animator))]
    public class RoomsChoice : MonoBehaviour
    {
        public static string EVT_ISKNOWN_ROOM = "RoomsChoice.EVT_ISKNOWN_ROOM";

        [SerializeField] Canvas _fadeCanvas;
        [SerializeField] AnimationClip _closingAnim;
        [SerializeField] RoomSelectionButton[] _roomSelectionButtons;
        [SerializeField] Sprite _lockRoomSprite;

        Animator _animator;
        
        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            Debug.Assert(_fadeCanvas != null, "_fadeCanvas cannot be null.");
            Debug.Assert(_closingAnim != null, "_closingAnim cannot be null.");

            _animator = GetComponent<Animator>();

            for (int i = 0; i < _roomSelectionButtons.Length; i++)
            {
                _roomSelectionButtons[i].Init(_lockRoomSprite);
            }
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        IEnumerator LaunchClosingAndChangeRoomCoroutine(string a_roomName)
        {
            yield return new WaitForSeconds(_closingAnim.length);
            SceneManager.LoadScene(a_roomName);
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void SetVisibility(bool a_show)
        {
            _animator.SetBool("Show", a_show);
            ActionsPanel.Instance.BlockInteractions(a_show);
        }
        /*********************************************************/

        public void Btn_CloseRoomsChoice()
        {
            SetVisibility(false);
        }
        /*********************************************************/

        public void Btn_ChangeRoom(string a_roomName)
        {
            _fadeCanvas.GetComponent<Animator>().SetTrigger("Closing");
            StartCoroutine(LaunchClosingAndChangeRoomCoroutine(a_roomName));
        }
        /*********************************************************/

    } // class
} // namespace
