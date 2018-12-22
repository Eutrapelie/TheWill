using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheWill
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] Button _startButton;
        [SerializeField] string _versionString;
        [SerializeField] Text _versionText;


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _startButton.onClick.AddListener(() => StartGame());
            SaveLoad.LoadSaves();
            SaveLoad.LoadOptions();
            Utils.Localization.InitializeLangDictionaries(Options.Current.GetLang());
            _versionText.text = _versionString;
        }
        /*********************************************************/

        void Start()
        {
            ParametersPanel.Instance.OnOptionsChanged += OnOptionsChanged;
        }
        /*********************************************************/

    ///////////////////////////////////////////////////////////////
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void StartGame()
        {
            SaveLoad.StartNewGame();
            SceneManager.LoadScene(Game.Current.currentRoom.ToString());
            SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);
        }
        /*********************************************************/

        void OnOptionsChanged()
        {
            Utils.Localization.InitializeLangDictionaries(Options.Current.GetLang());
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        // Called by "Reprendre" Button
        public void Btn_LoadFirstGame()
        {
            SaveLoad.Load(0);
            SceneManager.LoadScene(Game.Current.currentRoom.ToString());
            SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);
        }
        /*********************************************************/

        public void Btn_QuitGame()
        {
            Application.Quit();
        }
        /*********************************************************/
    }
}
