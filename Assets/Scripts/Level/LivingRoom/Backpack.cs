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
        Animator _animator;


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        /*********************************************************/

    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        #region Interface
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnMouseEnter != null)
                OnMouseEnter();

            if (_animator.enabled)
            {
                _animator.enabled = false;
                transform.rotation = Quaternion.identity;
                transform.localScale = Vector3.one;
            }
        }
        /*********************************************************/
        #endregion
    }
}
