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


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _startButton.onClick.AddListener(() => StartGame());
            SaveLoad.LoadSaves();
            SaveLoad.LoadOptions();
            Utils.Localization.InitializeLangDictionaries(Utils.Lang.fr);
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

        void Load()
        {

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
