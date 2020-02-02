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
        [SerializeField] AudioSource _musicSource;
        [SerializeField] LevelData _levelData_A1D1;


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
            _musicSource.volume = Options.Current.volume / 100f;
        }
        /*********************************************************/

        void Start()
        {
            ParametersPanel.Instance.OnOptionsChanged += OnOptionsChanged;
            ParametersPanel.Instance.OnCancelOptions += OnCancelOptions;
        }
        /*********************************************************/

    ///////////////////////////////////////////////////////////////
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void StartGame()
        {
            GameManager.StartNewGame(_levelData_A1D1);
            SceneManager.LoadScene(Game.Current.currentRoom.ToString());
            SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);
            //MusicManager.Instance.LaunchGame();
        }
        /*********************************************************/

        void OnOptionsChanged()
        {
            Utils.Localization.InitializeLangDictionaries(Options.Current.GetLang());
        }
        /*********************************************************/

        void OnCancelOptions()
        {
            _musicSource.volume = Options.Current.volume / 100f;
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
            //MusicManager.Instance.LaunchGame();
        }
        /*********************************************************/

        public void Btn_QuitGame()
        {
            Application.Quit();
        }
        /*********************************************************/

        public void Btn_UpdateVolume(float a_value)
        {
            _musicSource.volume = a_value / 100f;
        }
        /*********************************************************/
    }
}
