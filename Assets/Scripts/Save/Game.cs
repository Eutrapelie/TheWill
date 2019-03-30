using System;
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
                /*if (current.player != null)
                    Debug.Log("[Game] get Current - gender: " + current.player.genre);
                else
                    Debug.Log("[Game] get Current - not initialized");*/
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


        public void LoadStartLevel(int a_acte, int a_day)
        {
            // FOR TEST PURPOSE ONLY
            string level = "LevelAct" + a_acte + "Day" + a_day;
            //1/Debug.Log("[LevelController] Loading level : " + level);
            Debug.Log("levelController exists: " + levelControllerData != null);
            if (levelControllerData == null || levelControllerData.ActeNumber != a_acte || levelControllerData.DayNumber != a_day)
            {
                LevelController levelCtrl = ((LevelController)Resources.Load("Levels/CurrentLevel"));
                levelCtrl = new LevelController(((LevelController)Resources.Load("Levels/" + level)));
                levelControllerData = new LevelControllerData(levelCtrl.Data);
            }

            if (charactersInfos != null)
                for (int i = 0; i < charactersInfos.Count; i++)
                {
                    if (charactersInfos[i].characterName == Character.Geoffroy)
                        Debug.Log("// " + charactersInfos[i].characterName + ": " + charactersInfos[i].currentRoom);
                }
            charactersInfos = levelControllerData.Characters;
            for (int i = 0; i < charactersInfos.Count; i++)
            {
                if (charactersInfos[i].characterName == Character.Geoffroy)
                    Debug.Log("/// " + charactersInfos[i].characterName + ": " + charactersInfos[i].currentRoom);
            }

            player = levelControllerData.Kim;

            Debug.Log(currentRoom + " -- " + levelControllerData.StartRoom);
            currentRoom = levelControllerData.StartRoom;
            acteNumber = a_acte;
            dayNumber = a_day;
            roomVisited = new List<Room>();

            // FOR TEST PURPOSE ONLY
        }

        public void SaveGameFromManager()
        {
            realDateTime = DateTime.Now;
            player.codeLines = GameManager.Instance.PlayerController.PlayerChoices;
            //player.genre = GameManager.Instance.PlayerController.PlayerCard.Player.genre;
            Debug.Log("levelController exists: " + levelControllerData != null);

            Debug.Log(GameManager.Instance.PlayerController.CurrentRoom + " -- " + Game.Current.currentRoom);
            currentRoom = GameManager.Instance.PlayerController.CurrentRoom;

            acteNumber = GameManager.Instance.ActeNumber;
            dayNumber = GameManager.Instance.DayNumber;

            charactersInfos = new List<CharacterInfo>();
            foreach (CharacterCard card in GameManager.Instance.MyCharacterController.Characters)
            {
                Debug.Log(card.CharacterInfo);
                charactersInfos.Add(card.CharacterInfo);
            }
        }

        public string DebugGameData()
        {
            string gameData = "";
            gameData = "{Codeline: " + player.codeLines.Count + " -- Gender: " + player.genre + "\n" +
                "Room: " + currentRoom + " -- Acte: " + acteNumber + " -- Day: " + dayNumber + "}";
            return gameData;
        }

        public void LoadGame(Game a_game)
        {
            //charactersInfos = current.charactersInfos;
            player = a_game.player;
            Debug.Log(currentRoom + " -- " + a_game.currentRoom);
            currentRoom = a_game.currentRoom;
            acteNumber = a_game.acteNumber;
            dayNumber = a_game.dayNumber;

            /*if (levelControllerData == null)
            {*/
                string level = "LevelAct" + acteNumber + "Day" + dayNumber;
                LevelController levelCtrl = ((LevelController)Resources.Load("Levels/CurrentLevel"));
                levelCtrl = new LevelController(((LevelController)Resources.Load("Levels/" + level)));
                levelControllerData = new LevelControllerData(levelCtrl.Data);
            //}

            realDateTime = a_game.realDateTime;
            Debug.Log(realDateTime.ToShortDateString() + " " + realDateTime.ToShortTimeString());
            Debug.Log("[Game] LoadGame: " + DebugGameData());
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
                    Debug.Log(a_game.charactersInfos[i]);
                    charactersInfos.Remove(charInfo);
                    charactersInfos.Add(a_game.charactersInfos[i]);
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
    }
}
