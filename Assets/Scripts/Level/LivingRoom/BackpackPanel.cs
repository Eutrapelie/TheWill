using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [RequireComponent(typeof(Animator))]
    public class BackpackPanel : MonoBehaviour
    {
        Animator _animator;

        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        /*********************************************************/

        void Start() { }
        /*********************************************************/

        void Update() { }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void SetVisibility(bool a_show)
        {
            _animator.SetBool("Show", a_show);
        }
        /*********************************************************/
    }
}
