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
    public int acteNumber;
    public int dayNumber;

    public void Test()
    {
        // FOR TEST PURPOSE ONLY
        charactersInfos = new List<CharacterInfo>();
        CharacterInfo c1 = new CharacterInfo(Character.Abigail, Emotion.Neutral, Room.LivingRoom, Gauge.Courtesy, 0);
        charactersInfos.Add(c1);
        CharacterInfo c2 = new CharacterInfo(Character.Geoffroy, Emotion.Neutral, Room.LivingRoom, Gauge.Courtesy, 0);
        charactersInfos.Add(c2);
        CharacterInfo c3 = new CharacterInfo(Character.Ernest, Emotion.Neutral, Room.LivingRoom, Gauge.Frustration, 0);
        charactersInfos.Add(c3);
        CharacterInfo c4 = new CharacterInfo(Character.Hippolyte, Emotion.Neutral, Room.LivingRoom, Gauge.Irritation, 0);
        charactersInfos.Add(c4);
        CharacterInfo c5 = new CharacterInfo(Character.Leontine, Emotion.Neutral, Room.LivingRoom, Gauge.Courtesy, 0);
        charactersInfos.Add(c5);
        CharacterInfo c6 = new CharacterInfo(Character.Ophelie, Emotion.Neutral, Room.LivingRoom, Gauge.Depression, 0);
        charactersInfos.Add(c6);
        CharacterInfo c7 = new CharacterInfo(Character.Tayla, Emotion.Neutral, Room.None, Gauge.Frustration, 0);
        charactersInfos.Add(c7);

        List<CodeLine> codeLine = new List<CodeLine>();
        //codeLine.Add(CodeLine.DoNotMatterAbigail0000);
        //codeLine.Add(CodeLine.DoNotMatterGeoffroy0000);
        player = new Player("Kim", codeLine, new List<int>(), Genre.Man);

        acteNumber = 1;
        dayNumber = 1;

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

    public void LoadGame(Game game)
    {
        current = game;
    }
}
