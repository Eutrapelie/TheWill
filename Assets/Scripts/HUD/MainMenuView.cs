using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheWill
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        Button _startButton;


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _startButton.onClick.AddListener(() => StartGame());
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
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        // Called by "Charger une partie" Button
        public void Btn_LoadFirstGame()
        {
            SaveLoad.Load(0);
            SceneManager.LoadScene(Game.Current.currentRoom.ToString());
            SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);
        }
        /*********************************************************/
    }
}
