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
        [SerializeField]
        private CharacterInfo _characterInfo;

        [SerializeField]
        private List<CharacterSprite> _characterSprites;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private bool _isTalking;

        public event System.Action<Character> OnClickCharacterValue;

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

        public CharacterCard(CharacterInfo characterInfo)
        {
            CharacterInfo = characterInfo;
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

        private void OnClickObject(object value)
        {
            if (OnClickCharacterValue != null)
            {
                OnClickCharacterValue(CharacterInfo.characterName);
            }
        }

        void DoStuff(Character name)
        {
            Debug.Log("[DoStuff] name is " + name);
            Debug.Log("[DoStuff] GameManager.Instance.MyCharacterController " + GameManager.Instance.MyCharacterController);
            //GameManager.Instance.MyCharacterController.toggleAllCharactersEvent.Invoke(false);
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
            gameObject.SetActive(visible);
        }

        public void ChangeRoom(Room newRoom)
        {
            CharacterInfo.currentRoom = newRoom;
        }

        public void ChangeRoomSpot(RoomSpot newSpot)
        {
            CharacterInfo.currentRoomSpot = newSpot;
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
