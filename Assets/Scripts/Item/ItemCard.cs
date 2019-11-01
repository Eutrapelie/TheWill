using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    public class ItemCard : InteractibleObject
    {
        [SerializeField] SpriteRenderer _spriteRenderer;

        [SerializeField] bool _isTalking;
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

        [SerializeField] ItemInfo _info;
        public ItemInfo Info
        {
            get { return _info; }
            set { _info = value; }
        }

        public event Action<string> OnClickObjectValue;
        public bool isClicked = false;


        void Awake()
        {
            Sprite = _spriteRenderer;

            EventManager.StartListening(Info.name + "OnClick", OnClickObject);

            OnClickObjectValue += DoStuff;
        }

        void OnClickObject(object a_value)
        {
            if (OnClickObjectValue != null)
                OnClickObjectValue(Info.name);
        }

        void DoStuff(string a_name)
        {
            Debug.Log("[DoStuff] name is " + a_name);
            Debug.Log("[DoStuff] GameManager.Instance.MyCharacterController " + GameManager.Instance.MyCharacterController);
            //GameManager.Instance.MyCharacterController.toggleAllCharactersEvent.Invoke(false);
            isClicked = true;
        }
    }
}
