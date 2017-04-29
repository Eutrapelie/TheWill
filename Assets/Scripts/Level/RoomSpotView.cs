using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpotView : MonoBehaviour {

    [SerializeField]
    private RoomSpot _roomSpot;

    [SerializeField]
    private bool _spotAvailable = true;

    public RoomSpot RoomSpots
    {
        get
        {
            return _roomSpot;
        }

        set
        {
            _roomSpot = value;
        }
    }

    public bool SpotAvailable
    {
        get
        {
            return _spotAvailable;
        }

        set
        {
            _spotAvailable = value;
        }
    }
}
