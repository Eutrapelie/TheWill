using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    public class ChangeSpriteHierarchyVisibility : MonoBehaviour
    {
        SpriteRenderer[] _spritesRenderer;
        bool _enabled;


        public void SetSpritesVisibility(bool a_enabled)
        {
            _enabled = a_enabled;
            _spritesRenderer = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in _spritesRenderer)
            {
                sr.enabled = _enabled;
            }
        }
        /*********************************************************/
    }
}
