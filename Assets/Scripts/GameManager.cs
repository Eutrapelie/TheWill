using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TheWill
{
    public class GameManager : MonoBehaviour
    {
        MyCharacterController myCharacterController;
        public MyCharacterController MyCharacterController
        {
            get
            {
                if (myCharacterController == null)
                {
                    myCharacterController = gameObject.GetComponent<MyCharacterController>();
                }
                return myCharacterController;
            }

            set
            {
                myCharacterController = value;
            }
        }
        PlayerController playerController;
        public PlayerController PlayerController
        {
            get
            {
                if (playerController == null)
                {
                    playerController = gameObject.GetComponent<PlayerController>();
                }
                return playerController;
            }

            set
            {
                playerController = value;
            }
        }

        int acteNumber;
        public int ActeNumber
        {
            get
            {
                return acteNumber;
            }

            set
            {
                acteNumber = value;
            }
        }
        int dayNumber;
        public int DayNumber
        {
            get
            {
                return dayNumber;
            }

            set
            {
                dayNumber = value;
            }
        }
        
        static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GlobalBlackboard go = FindObjectOfType<GlobalBlackboard>();
                    if (go != null)
                    {
                        instance = go.gameObject.AddComponent<GameManager>();
                        Instance.ActeNumber = Game.Current.acteNumber;
                        Instance.DayNumber = Game.Current.dayNumber;
                        Instance.LoadPlayerInfo();
                    }
                }
                return instance;
            }
        }

        public bool allowClickOnObject;


        void Start()
        {
            Debug.Log("######### start");
            if (MusicManager.Instance.gameObject.GetComponent<AudioSource>() == null)
                MusicManager.Instance.PlayAudioSource(MusicManager.MAIN_MUSIC);
        }

        public static void StartNewGame(LevelData a_levelData)
        {
            Game.Current.LoadStartLevel(a_levelData);
        }

        public static void DestroyInstance()
        {
            if (instance == null)
                return;

            Destroy(MusicManager.Instance.gameObject);
            Destroy(instance.gameObject);
            instance = null;
        }

        void LoadPlayerInfo()
        {
            Debug.Log("[GameManager] LoadPlayerInfo: " + Game.Current.player.codeLines.Count);
            Instance.PlayerController.PlayerChoices = Game.Current.player.codeLines;
            Instance.PlayerController.CurrentRoom = Game.Current.currentRoom;
            if (Instance.PlayerController.PlayerCard != null)
            {
                Debug.Log("[GameManager] LoadPlayerInfo: " + Game.Current.player.genre);
                Instance.PlayerController.PlayerCard.Player = Game.Current.player;
            }
        }
    }
}
