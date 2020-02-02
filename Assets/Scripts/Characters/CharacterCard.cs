using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.BehaviourTrees;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TheWill
{
    public class CharacterCard : InteractibleObject
    {
        [SerializeField] CharacterInfo _characterInfo;
        [SerializeField] List<CharacterSprite> _characterSprites;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] bool _isTalking;

        public event Action<string> OnClickCharacterValue;

        public CharacterInfo CharacterInfo
        {
            get
            {
                return _characterInfo;
            }

            set
            {
                _characterInfo = value;
            }
        }

        public bool IsTalking
        {
            get
            {
                return _isTalking;
            }

            set
            {
                _isTalking = value;
            }
        }

        public bool isClicked = false;

        public CharacterCard(CharacterInfo a_characterInfo)
        {
            CharacterInfo = a_characterInfo;
        }

        void Update()
        {
            /* if(Input.GetKeyUp(KeyCode.A))
             {
                 if (OnClickCharacterValue != null)
                 {
                     OnClickCharacterValue(_characterInfo.characterName);
                 }
             }*/
        }

        void Awake()
        {
            Sprite = _spriteRenderer;

            EventManager.StartListening(CharacterInfo.characterName.ToString() + "OnClick", OnClickObject);

            OnClickCharacterValue += DoStuff;
        }

        void OnClickObject(object a_value)
        {
            if (OnClickCharacterValue != null)
                OnClickCharacterValue(CharacterInfo.characterName.ToString());
        }

        void DoStuff(string a_name)
        {
            Debug.Log("[DoStuff] name is " + a_name);
            //GameManager.Instance.MyCharacterController.toggleAllCharactersEvent.Invoke(false);
            isClicked = true;
        }

        public void ChangeSprite()
        {
            string sprite = CharacterInfo.characterName.ToString() + CharacterInfo.currentEmotion.ToString() + (_isTalking ? "Talking" : "");

            foreach (CharacterSprite item in _characterSprites)
            {
                string spriteItem = CharacterInfo.characterName.ToString() + item.Emotion + (item.Talking ? "Talking" : "");
                if (spriteItem.Equals(sprite))
                {
                    _spriteRenderer.sprite = item.Sprite;
                    break;
                }
            }
        }

        public void ChangeCurrentEmotion(Emotion arg0)
        {
            CharacterInfo.currentEmotion = arg0;
            ChangeSprite();
        }

        public void ToggleVisibility(bool visible)
        {
            Debug.Log("ToggleVisibility of " + CharacterInfo.characterName +  " to " + visible);
            gameObject.SetActive(visible);
            isClicked = false;
        }

        public void ChangeRoom(Room newRoom)
        {
            CharacterInfo.currentRoom = newRoom;
            Game.Current.levelControllerData.ChangeRoomOfCharacter(CharacterInfo.characterName, newRoom);
        }

        public void ChangeRoomSpot(RoomSpot newSpot)
        {
            CharacterInfo.currentRoomSpot = newSpot;
            Game.Current.levelControllerData.ChangeRoomSpotOfCharacter(CharacterInfo.characterName, newSpot);
        }

        public void SetGauge(int value)
        {
            CharacterInfo.valueGauge += value;
        }
    }

    [System.Serializable]
    public class CharacterSprite
    {
        public Emotion Emotion;

        public bool Talking;

        public Sprite Sprite;
    }
}
