using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [RequireComponent(typeof(Animator))]
    public class BackpackPanel : MonoBehaviour
    {
        Animator _animator;


        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Start()
        {

        }
        
        void Update()
        {

        }

        public void SetVisibility(bool a_show)
        {
            _animator.SetBool("Show", a_show);
        }
    }
}
