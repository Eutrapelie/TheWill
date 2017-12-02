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


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Start() { }
        /*********************************************************/

        void Update() { }
        /*********************************************************/

    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        #region Interface
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnMouseEnter != null)
                OnMouseEnter();
        }
        /*********************************************************/
        #endregion
    }
}
