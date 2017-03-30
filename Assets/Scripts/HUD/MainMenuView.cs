using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    void Awake()
    {
        _startButton.onClick.AddListener(() => StartGame());
    }

    void StartGame()
    {
        SaveLoad.StartNewGame();
        SceneManager.LoadScene(Game.Current.currentRoom.ToString());
        SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);
    }

    void Load()
    {

    }
}
