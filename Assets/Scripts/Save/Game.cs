﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [System.Serializable]
    public class Game
    {
        static Game current;
        public static Game Current
        {
            get
            {
                if (current == null)
                {
                    current = new Game();
                }
                /*if (current.levelControllerData != null)
                    Debug.Log("[Game] get Current - gender: " + current.levelControllerData.name);
                else
                    Debug.Log("[Game] get Current - not initialized");//*/
                return current;
            }
        }

        Game() { }

        public Player player;
        public List<CharacterInfo> charactersInfos = new List<CharacterInfo>();
        public Room currentRoom;
        public List<Room> roomVisited = new List<Room>();
        public int acteNumber = 1;
        public int dayNumber = 1;
        public DateTime realDateTime;
        public DateTime gameDateTime;
        public LevelControllerData levelControllerData;
        public int backpackElement = 0;


        /*public void LoadStartLevel(int a_acte, int a_day)
        {
            // FOR TEST PURPOSE ONLY
            string level = "LevelAct" + a_acte + "Day" + a_day;
            //1/Debug.Log("[LevelController] Loading level : " + level);
            Debug.Log("levelController exists: " + (levelControllerData != null));
            if (levelControllerData == null || levelControllerData.ActeNumber != a_acte || levelControllerData.DayNumber != a_day)
            {
                LevelController levelCtrl = ((LevelController)Resources.Load("Levels/CurrentLevel"));
                Debug.Log(levelCtrl.name);
                levelCtrl.Data = new LevelControllerData(((LevelController)Resources.Load("Levels/" + level)).Data);
                levelCtrl = ((LevelController)Resources.Load("Levels/CurrentLevel"));
                Debug.Log(levelCtrl.name);
                levelControllerData = levelCtrl.Data;
            }

            if (charactersInfos != null)
                for (int i = 0; i < charactersInfos.Count; i++)
                {
                    if (charactersInfos[i].characterName == Character.Geoffroy)
                        Debug.Log("<color=purple>// " + charactersInfos[i].characterName + ": " + charactersInfos[i].currentRoom + "</color>");
                }
            charactersInfos = levelControllerData.Characters;
            for (int i = 0; i < charactersInfos.Count; i++)
            {
                if (charactersInfos[i].characterName == Character.Geoffroy)
                    Debug.Log("<color=purple>/// " + charactersInfos[i].characterName + ": " + charactersInfos[i].currentRoom + "</color>");
            }

            player = levelControllerData.Kim;

            Debug.Log(currentRoom + " -- " + levelControllerData.StartRoom);
            currentRoom = levelControllerData.StartRoom;
            acteNumber = a_acte;
            dayNumber = a_day;
            roomVisited = new List<Room>();

            // FOR TEST PURPOSE ONLY
        }
        /*********************************************************/

        public void LoadStartLevel(LevelData a_levelData)
        {
            //1/Debug.Log("[LevelController] Loading level : " + level);
            Debug.Log("levelControllerData exists: " + (levelControllerData != null));
            if (levelControllerData == null || levelControllerData.ActeNumber != a_levelData.acteNumber || levelControllerData.DayNumber != a_levelData.dayNumber)
            {
                LevelController levelCtrl = ((LevelController)Resources.Load("Levels/CurrentLevel"));
                levelCtrl.Data = new LevelControllerData(a_levelData);
                Debug.Log(a_levelData.name);
                levelControllerData = levelCtrl.Data;
            }

            if (charactersInfos != null)
                for (int i = 0; i < charactersInfos.Count; i++)
                {
                    if (charactersInfos[i].characterName == Character.Geoffroy)
                        Debug.Log("<color=purple>// " + charactersInfos[i].characterName + ": " + charactersInfos[i].currentRoom + "</color>");
                }
            charactersInfos = levelControllerData.Characters;
            for (int i = 0; i < charactersInfos.Count; i++)
            {
                if (charactersInfos[i].characterName == Character.Geoffroy)
                    Debug.Log("<color=purple>/// " + charactersInfos[i].characterName + ": " + charactersInfos[i].currentRoom + "</color>");
            }

            player = levelControllerData.Kim;

            Debug.Log(currentRoom + " -- " + levelControllerData.StartRoom);
            currentRoom = levelControllerData.StartRoom;
            acteNumber = a_levelData.acteNumber;
            dayNumber = a_levelData.dayNumber;
            roomVisited = new List<Room>();
        }
        /*********************************************************/

        public void SaveGameFromManager()
        {
            realDateTime = DateTime.Now;
            player.codeLines = GameManager.Instance.PlayerController.PlayerChoices;
            //player.genre = GameManager.Instance.PlayerController.PlayerCard.Player.genre;
            Debug.Log("levelController exists: " + (levelControllerData != null));

            Debug.Log(GameManager.Instance.PlayerController.CurrentRoom + " -- " + Game.Current.currentRoom);
            currentRoom = GameManager.Instance.PlayerController.CurrentRoom;

            acteNumber = GameManager.Instance.ActeNumber;
            dayNumber = GameManager.Instance.DayNumber;

            charactersInfos = new List<CharacterInfo>();
            foreach (CharacterCard card in GameManager.Instance.MyCharacterController.Characters)
            {
                Debug.Log("<color=purple>" +card.CharacterInfo + "</color>");
                charactersInfos.Add(card.CharacterInfo);
            }
        }
        /*********************************************************/

        public string DebugGameData()
        {
            string gameData = "";
            gameData = "{Codeline: " + player.codeLines.Count + " -- Gender: " + player.genre + "\n" +
                "Room: " + currentRoom + " -- Acte: " + acteNumber + " -- Day: " + dayNumber + "}";
            return gameData;
        }
        /*********************************************************/

        public void LoadGame(Game a_game)
        {
            //charactersInfos = current.charactersInfos;
            player = a_game.player;
            Debug.Log("[LoadGame] " + currentRoom + "-- " + a_game.currentRoom);
            currentRoom = a_game.currentRoom;
            acteNumber = a_game.acteNumber;
            dayNumber = a_game.dayNumber;

            string level = "LevelAct" + acteNumber + "Day" + dayNumber;
            LevelController levelCtrl = ((LevelController)Resources.Load("Levels/CurrentLevel"));
            levelCtrl = new LevelController(((LevelController)Resources.Load("Levels/" + level)));
            levelControllerData = new LevelControllerData(levelCtrl.Data);

            realDateTime = a_game.realDateTime;
            Debug.Log(realDateTime.ToShortDateString() + " " + realDateTime.ToShortTimeString());
            Debug.Log("[Game] LoadGame: " + DebugGameData());

            LoadLocalization();

            roomVisited.Clear();
            if (a_game.roomVisited != null)
                foreach (Room room in a_game.roomVisited)
                    roomVisited.Add(room);

            charactersInfos.Clear();
            charactersInfos = levelControllerData.Characters;
            for (int i = 0; i < a_game.charactersInfos.Count; i++)
            {
                CharacterInfo charInfo = charactersInfos.Find(c => c.characterName == a_game.charactersInfos[i].characterName);
                if (charInfo != null)
                {
                    Debug.Log("<color=purple>"+a_game.charactersInfos[i]+"</color>");
                    charactersInfos.Remove(charInfo);
                    charactersInfos.Insert(i, a_game.charactersInfos[i]);
                }
            }

            backpackElement = a_game.backpackElement;
            //GameManager.Instance.PlayerController.PlayerChoices = current.player.codeLines;
            //GameManager.Instance.PlayerController.PlayerCard.Player.genre = current.player.genre;

            /*charactersInfos = new List<CharacterInfo>();
            foreach (CharacterCard card in GameManager.Instance.MyCharacterController.Characters)
            {
                charactersInfos.Add(card.CharacterInfo);
            }*/
        }
        /*********************************************************/

        void LoadLocalization()
        {
            if (acteNumber == 1)
            {
                if (dayNumber == 1)
                    Utils.Localization.InitializeLangDictionaries(Options.Current.GetLang(), 0, 1);
                else if (dayNumber == 2)
                    Utils.Localization.InitializeLangDictionaries(Options.Current.GetLang(), 2, 2);
            }
        }
        /*********************************************************/
    }
}
