using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
    private static Game current;
    private Game() { }

    public static Game Current
    {
        get
        {
            if (current == null)
            {
                current = new Game();
            }
            return current;
        }
    }

    public Player player;
    public List<CharacterInfo> charactersInfos;
    public Room currentRoom;
    public List<Room> roomVisited;
    public int acteNumber = 1;
    public int dayNumber = 1;

    public void LoadStartLevel(int acte, int day)
    {
        // FOR TEST PURPOSE ONLY
        string level = "LevelAct" + acte + "Day" + day;
        Debug.Log("[LevelController] Loading level : " + level);
        LevelController levelController = ((LevelController)Resources.Load("Levels/" + level));
        if (levelController == null)
        {
            Debug.Log("[LevelController] Level load failed");
        }
        else
        {
            Debug.Log("[LevelController] Level load succeed");
        }

        charactersInfos = levelController.Characters;
        player = levelController.Kim;
        currentRoom = levelController.StartRoom;
        acteNumber = acte;
        dayNumber = day;

        // FOR TEST PURPOSE ONLY
    }

    public void SaveGameFromManager()
    {
        player.codeLines = GameManager.Instance.PlayerController.PlayerChoices;
        player.genre = GameManager.Instance.PlayerController.PlayerCard.Player.genre;

        currentRoom = GameManager.Instance.PlayerController.CurrentRoom;

        acteNumber = GameManager.Instance.ActeNumber;
        dayNumber = GameManager.Instance.DayNumber;

        charactersInfos = new List<CharacterInfo>();
        foreach (CharacterCard card in GameManager.Instance.MyCharacterController.Characters)
        {
            charactersInfos.Add(card.CharacterInfo);
        }
    }

    public string DebugGameData()
    {
        string gameData = "";
        gameData = "{Codeline: " + player.codeLines.Count + " -- Gender: " + player.genre +"\n" +
            "Room: " + currentRoom + " -- Acte: " + acteNumber + " -- Day: " + dayNumber + "}";
        return gameData;
    }

    public void LoadGame(Game a_game)
    {
        current = a_game;
        //GameManager.Instance.PlayerController.PlayerChoices = current.player.codeLines;
        //GameManager.Instance.PlayerController.PlayerCard.Player.genre = current.player.genre;
        
        /*charactersInfos = new List<CharacterInfo>();
        foreach (CharacterCard card in GameManager.Instance.MyCharacterController.Characters)
        {
            charactersInfos.Add(card.CharacterInfo);
        }*/
    }
}
