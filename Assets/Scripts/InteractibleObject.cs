using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _sprite = null;

    public SpriteRenderer Sprite
    {
        get
        {
            return _sprite;
        }

        set
        {
            _sprite = value;
        }
    }
}
