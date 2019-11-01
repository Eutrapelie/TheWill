using UnityEngine;

namespace TheWill
{
    public class InteractibleObject : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _sprite = null;

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
}
