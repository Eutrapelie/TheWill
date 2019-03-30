using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheWill
{
    [RequireComponent(typeof(Animator))]
    public class SaveLoadPanel : MonoBehaviour
    {
        [SerializeField] Button _previousPageButton;
        [SerializeField] Button _nextPageButton;
        [SerializeField] Text _pageCounter;

        Animator _animator;
        bool _isLoadPanel;
        public void SetIsLoadPanel(bool a_value) { _isLoadPanel = a_value; }

        int _currentPage;
        [SerializeField] int _pageNumberMax = 9;
        [SerializeField] List<Button> _slotButtons;
        List<Image> _slotImages;
        List<Text> _slotTexts;

        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            Debug.Assert(_previousPageButton != null, "_previousPageButton have to be set");
            Debug.Assert(_nextPageButton != null, "_nextPageButton have to be set");
            Debug.Assert(_pageCounter != null, "_pageCounter have to be set");

            _slotImages = new List<Image>();
            _slotTexts = new List<Text>();
            foreach (Button btn in _slotButtons)
            {
                _slotImages.Add(btn.image);
                _slotTexts.Add(btn.GetComponentInChildren<Text>());
            }
        }
        /*********************************************************/

        void Start()
        {
            _currentPage = 1;
            _previousPageButton.interactable = false;
            _animator = GetComponent<Animator>();
            UpdateSwitchPanel();
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void LoadSlots()
        {
            for (int i = 0; i < _slotButtons.Count; i++)
            {
                int loadIndex = i + (_currentPage - 1) * 9;
                _slotButtons[i].onClick.RemoveAllListeners();

                if (_isLoadPanel)
                {
                    if (SaveLoad.savedGames[loadIndex] != null)
                    {
                        _slotButtons[i].interactable = true;
                        Game game = SaveLoad.savedGames[loadIndex];
                        _slotTexts[i].text = game.realDateTime.ToShortDateString() + " - " + game.realDateTime.ToShortTimeString() + "\n" +
                            game.currentRoom + " - " + game.gameDateTime.ToShortDateString() + " - " + game.gameDateTime.ToShortTimeString() + "\n" + 
                            game.player.genre;
                        Sprite thumbSprite = Resources.Load<Sprite>("Sprites/UI/Rooms_thumbs/" + game.currentRoom + "_thumb");
                        _slotImages[i].sprite = thumbSprite;
                        _slotButtons[i].onClick.AddListener(
                            () => Btn_SelectLoadSlot(loadIndex)
                        );
                    }
                    else
                    {
                        _slotButtons[i].interactable = false;
                        _slotTexts[i].text = string.Format(Constants.LOAD_SLOT_FORMAT, loadIndex + 1);
                        _slotImages[i].sprite = null;
                    }
                } else
                {
                    if (SaveLoad.savedGames[loadIndex] != null)
                    {
                        Game game = SaveLoad.savedGames[loadIndex];
                        _slotTexts[i].text = game.realDateTime.ToShortDateString() + " - " + game.realDateTime.ToShortTimeString() + "\n" +
                            game.currentRoom + " - " + game.gameDateTime.ToShortDateString() + " - " + game.gameDateTime.ToShortTimeString() + "\n" + 
                            game.player.genre;
                        Sprite thumbSprite = Resources.Load<Sprite>("Sprites/UI/Rooms_thumbs/" + game.currentRoom + "_thumb");
                        _slotImages[i].sprite = thumbSprite;
                    } else
                    {
                        _slotTexts[i].text = string.Format(Constants.LOAD_SLOT_FORMAT, loadIndex + 1);
                        _slotImages[i].sprite = null;
                    }
                    _slotButtons[i].interactable = true;
                    _slotButtons[i].onClick.AddListener(
                        () => Btn_SelectSaveSlot(loadIndex)
                    );
                }
            }
        }
        /*********************************************************/

        void UpdateSwitchPanel()
        {
            _previousPageButton.interactable = (_currentPage != 1);
            _nextPageButton.interactable = (_currentPage != _pageNumberMax);
            _pageCounter.text = _currentPage + " / " + _pageNumberMax;
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void Btn_SelectLoadSlot(int a_slotIndex)
        {
            Debug.Log("SelectLoadSlot - " + a_slotIndex);
            GameManager.DestroyInstance();
            SaveLoad.Load(a_slotIndex);
            SceneManager.LoadScene(Game.Current.currentRoom.ToString());
            SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);
        }
        /*********************************************************/

        public void Btn_SelectSaveSlot(int a_slotIndex)
        {
            Debug.Log("SelectSaveSlot - " + a_slotIndex);
            SaveLoad.Save(a_slotIndex);
            PausePanel.Instance.UpdateDisplayAfterSave();
            Btn_HideSaveLoadPanel();
        }
        /*********************************************************/

        public void Btn_ChangeToPreviousPage()
        {
            _currentPage--;
            UpdateSwitchPanel();
            LoadSlots();
        }
        /*********************************************************/

        public void Btn_ChangeToNextPage()
        {
            _currentPage++;
            UpdateSwitchPanel();
            LoadSlots();
        }
        /*********************************************************/
        
        public void Btn_DisplaySaveLoadPanel(bool a_isLoadPanel)
        {
            _animator.SetBool("Show", true);
            _isLoadPanel = a_isLoadPanel;
            LoadSlots();
        }
        /*********************************************************/

        public void Btn_HideSaveLoadPanel()
        {
            _animator.SetBool("Show", false);
        }
        /*********************************************************/
    }
}
