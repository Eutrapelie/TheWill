﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToSceneButton : MonoBehaviour
{
    [SerializeField]
    private Room _room;

    void Start()
    {
        Button button = this.gameObject.GetComponent<Button>();
        if (button)
        {
            button.onClick.AddListener(() => GoToRoom(_room));
        }
    }

    private void GoToRoom(Room room)
    {
        SceneManager.LoadScene(room.ToString());
    }
}
