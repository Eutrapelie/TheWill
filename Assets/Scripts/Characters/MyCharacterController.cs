﻿using NodeCanvas.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TheWill
{
    public class MyCharacterController : MonoBehaviour
    {
        [SerializeField]
        private List<CharacterCard> _characters;

        [SerializeField]
        private GlobalBlackboard _globalBlackboard;

        private CharacterCard talkingCharacter;

        public List<CharacterCard> Characters
        {
            get
            {
                return _characters;
            }

            set
            {
                _characters = value;
            }
        }

        void Awake()
        {
            talkingCharacter = null;

            foreach (Character character in Enum.GetValues(typeof(Character)))
            {
                EventManager.StartListening(character.ToString() + "OnClick", HideAllCharacters);
            }

            EventManager.StartListening(VNFinishNode.EVT_FINISH_DIALOG, ShowAllCharacters);
            EventManager.StartListening(ToggleCharacterApparition.EVT_TOGGLE_CHARACTER_APPARITION, CharacterApparition);
            EventManager.StartListening(VN.Dialog.StatmentNodeVN.EVT_CHARACTER_TALKING, CharacterIsTalking);

            EventManager.StartListening(ChangeRoomForCharacter.EVT_CHARACTER_CHANGE_ROOM, CharacterChangeRoom);
            EventManager.StartListening(ChangeRoomForCharacter.EVT_CHARACTER_CHANGE_ROOM_SPOT, CharacterChangeRoomSpot);
            EventManager.StartListening(ChangeEmotionForCharacter.EVT_CHARACTER_CHANGE_EMOTION, CharacterChangeEmotion);
            EventManager.StartListening(SetGauge.EVT_CHARACTER_SET_GAUGE, CharacterSetGauge);

            EventManager.StartListening(ChangeRoomForKim.EVT_KIM_CHANGE_ROOM, KimChangeRoom);
        }

        void OnDestroy()
        {
            foreach (Character character in Enum.GetValues(typeof(Character)))
            {
                EventManager.StopListening(character.ToString() + "OnClick", HideAllCharacters);
            }

            EventManager.StopListening(VNFinishNode.EVT_FINISH_DIALOG, ShowAllCharacters);
            EventManager.StopListening(ToggleCharacterApparition.EVT_TOGGLE_CHARACTER_APPARITION, CharacterApparition);
            EventManager.StopListening(VN.Dialog.StatmentNodeVN.EVT_CHARACTER_TALKING, CharacterIsTalking);

            EventManager.StopListening(ChangeRoomForCharacter.EVT_CHARACTER_CHANGE_ROOM, CharacterChangeRoom);
            EventManager.StopListening(ChangeRoomForCharacter.EVT_CHARACTER_CHANGE_ROOM_SPOT, CharacterChangeRoomSpot);
            EventManager.StopListening(ChangeEmotionForCharacter.EVT_CHARACTER_CHANGE_EMOTION, CharacterChangeEmotion);
            EventManager.StopListening(SetGauge.EVT_CHARACTER_SET_GAUGE, CharacterSetGauge);
            EventManager.StopListening(ChangeRoomForKim.EVT_KIM_CHANGE_ROOM, KimChangeRoom);
        }

        public CharacterCard GetCharacterCardGameObject(Character character)
        {
            foreach (CharacterCard card in _characters)
            {
                if (card.CharacterInfo.characterName.Equals(character))
                {
                    return card;
                }
            }
            return null;
        }

        public void HideAllCharacters(object value)
        {
            CharacterCard card = (CharacterCard)value;
            if (!card)
            {
                Debug.LogError("[MyCharacterController]Error from event to hide all characters");
                return;
            }

            /*Character character = (Character)Enum.Parse(typeof(Character), ((string)characterName), true);
            CharacterCard talkingCard = GetCharacterCardGameObject(character);
            RoomSpot spot = (RoomSpot)Enum.Parse(typeof(RoomSpot), (talkingCard.CharacterInfo.currentRoom.ToString() + "UpSpot1"), true);
            GetCharacterToUpSpot(talkingCard, spot);*/

            EventManager.TriggerEvent(MainController.EVT_UPSPOT_CHARACTER, card);

            foreach (CharacterCard character in _characters)
            {
                character.ToggleVisibility(false);
            }

        }

        public void ShowAllCharacters(object value)
        {
            Debug.Log("[MyCharacterController] ShowAllCharacters");
            foreach (CharacterCard character in _characters)
            {
                character.ToggleVisibility(true);
            }

        }

        public void AddCharacterCardToGlobalBB(CharacterCard card)
        {
            Variable found = _globalBlackboard.GetVariable(card.CharacterInfo.characterName + "Card");
            if (found != null)
            {
                found.value = card;
            }
        }

        void CharacterIsTalking(object arg0)
        {
            Character character = (Character)arg0;
            if (talkingCharacter != null && talkingCharacter.CharacterInfo.characterName.Equals(arg0))
            {
                return;
            }

            foreach (CharacterCard item in Characters)
            {
                if (item.IsTalking && !item.CharacterInfo.characterName.Equals(arg0))
                {
                    item.IsTalking = false;
                    Debug.Log("[MyCharacterController] " + item.CharacterInfo.characterName + " stops talking");
                    talkingCharacter = item;
                    item.ChangeSprite();
                }
                else if (!item.IsTalking && item.CharacterInfo.characterName.Equals(arg0))
                {
                    item.IsTalking = true;
                    Debug.Log("[MyCharacterController] " + item.CharacterInfo.characterName + " is talking");
                    talkingCharacter = item;
                    item.ChangeSprite();
                }
            }
        }

        void CharacterApparition(object args)
        {
            List<object> list = (List<object>)args;
            Character character = (Character)list[0];
            bool visible = (bool)list[1];
            foreach (CharacterCard item in Characters)
            {
                if (item.CharacterInfo.characterName.Equals(character))
                {
                    item.ToggleVisibility(visible);
                }
            }
        }

        void KimChangeRoom(object arg0)
        {
            Room newRoom = (Room)arg0;
            switch (newRoom)
            {
                case Room.None:
                    break;
                case Room.Entrance:
                    SceneManager.LoadScene("Entrance");
                    break;
                case Room.LivingRoom:
                    SceneManager.LoadScene("LivingRoomIntegration");
                    break;
                case Room.Garden:
                    SceneManager.LoadScene("Garden");
                    break;
                case Room.Hall:
                    SceneManager.LoadScene("Hall");
                    break;
                default:
                    break;
            }
            SceneManager.LoadScene("InGameMenu", LoadSceneMode.Additive);

            if (newRoom != Room.None)
            {
                Destroy(gameObject);
                Game.Current.currentRoom = newRoom;
            }
        }

        void CharacterChangeRoom(object args)
        {
            List<object> list = (List<object>)args;
            Character arg0 = (Character)list[0];
            Room arg1 = (Room)list[1];
            foreach (CharacterCard item in Characters)
            {
                if (item.CharacterInfo.characterName.Equals(arg0))
                {
                    item.ChangeRoom(arg1);
                    break;
                }
            }
        }

        void CharacterChangeRoomSpot(object args)
        {
            List<object> list = (List<object>)args;
            Character arg0 = (Character)list[0];
            RoomSpot arg1 = (RoomSpot)list[1];
            //RoomSpot room = (RoomSpot)Enum.Parse(typeof(RoomSpot), arg1, true);
            CharacterChangeRoomSpot(arg0, arg1);
        }

        void CharacterChangeRoomSpot(Character arg0, RoomSpot arg1)
        {
            foreach (CharacterCard item in Characters)
            {
                if (item.CharacterInfo.characterName.Equals(arg0))
                {
                    item.ChangeRoomSpot(arg1);
                    break;
                }
            }
        }

        void CharacterChangeEmotion(object args)
        {
            List<object> list = (List<object>)args;
            Debug.Log("[MyCharacterController] CharacterChangeEmotion -- args.Count: " + list.Count);
            Character arg0 = (Character)list[0];
            Emotion arg1 = (Emotion)list[1];
            Debug.Log("\tCharacter: " + arg0.ToString() + " - Emotion: " + arg1);
            foreach (CharacterCard item in Characters)
            {
                if (item.CharacterInfo.characterName.Equals(arg0))
                {
                    item.ChangeCurrentEmotion(arg1);
                    break;
                }
            }
        }

        void CharacterSetGauge(object args)
        {
            List<object> list = (List<object>)args;
            Character arg0 = (Character)list[0];
            int arg1 = (int)list[1];
            foreach (CharacterCard item in Characters)
            {
                if (item.CharacterInfo.characterName.Equals(arg0))
                {
                    item.SetGauge(arg1);
                    break;
                }
            }
        }

    }
}
