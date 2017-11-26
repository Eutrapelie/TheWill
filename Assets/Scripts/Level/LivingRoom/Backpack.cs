using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheWill
{
    public class Backpack : MonoBehaviour, IPointerEnterHandler
    {
        public Action OnMouseEnter;


        void Start()
        {

        }
        
        void Update()
        {

        }

        #region Interface
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnMouseEnter != null)
                OnMouseEnter();
        }
        #endregion
    }
}
