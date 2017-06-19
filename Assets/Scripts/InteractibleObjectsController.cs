using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObjectsController : MonoBehaviour
{    
    private InteractibleObject _selectedObject = null;

    public InteractibleObject SelectedObject
    {
        get
        {
            return _selectedObject;
        }

        set
        {
            _selectedObject = value;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
            if (hitInfo)
            {
                SelectedObject = hitInfo.transform.gameObject.GetComponent<InteractibleObject>();

                CharacterCard character = (CharacterCard)SelectedObject;
                if (character)
                {
                    // Here you can check hitInfo to see which collider has been hit, and act appropriately.
                    EventManager.TriggerEvent(character.CharacterInfo.characterName.ToString());
                }
            }
        }
    }
}
