using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelObjectController : MonoBehaviour {

    [SerializeField]
    private InteractibleObjectsController _interactibleObjectsController = null;

    [SerializeField]
    private Image _imageObject = null;

	// Use this for initialization
	void Awake () {
        this.gameObject.SetActive(false);
        EventManager.StartListening("ShowObject", () => { ShowObject(); });
	}

    private void ShowObject()
    {
        this.gameObject.SetActive(true);
        InteractibleObject selectedObject = _interactibleObjectsController.SelectedObject;
        _imageObject.overrideSprite = selectedObject.Sprite.sprite;
        Debug.Log("well hello " + selectedObject.name);
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
    
}
