using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private MyCharacterController myCharacterController;
    private PlayerController playerController;
    private int acteNumber;
    private int dayNumber;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GlobalBlackboard go = FindObjectOfType<GlobalBlackboard>();
                if (go != null)
                {
                    instance = go.gameObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public MyCharacterController MyCharacterController
    {
        get
        {
            if(myCharacterController == null)
            {
                myCharacterController = gameObject.GetComponent<MyCharacterController>();
            }
            return myCharacterController;
        }

        set
        {
            myCharacterController = value;
        }
    }

    public PlayerController PlayerController
    {
        get
        {
            if(playerController == null)
            {
                playerController = gameObject.GetComponent<PlayerController>();
            }
            return playerController;
        }

        set
        {
            playerController = value;
        }
    }

    public int ActeNumber
    {
        get
        {
            return acteNumber;
        }

        set
        {
            acteNumber = value;
        }
    }

    public int DayNumber
    {
        get
        {
            return dayNumber;
        }

        set
        {
            dayNumber = value;
        }
    }
}
