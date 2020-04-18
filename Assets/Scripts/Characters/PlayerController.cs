using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<CodeLine> _playerChoices;
    [SerializeField] List<TheWill.ItemInfo> _playerItems;
    [SerializeField] PlayerCard _playerCard;
    [SerializeField] Room _currentRoom;

    public List<CodeLine> PlayerChoices
    {
        get
        {
            return _playerChoices;
        }

        set
        {
            _playerChoices = value;
        }
    }

    public List<TheWill.ItemInfo> PlayerItems
    {
        get
        {
            return _playerItems;
        }

        set
        {
            _playerItems = value;
        }
    }

    public PlayerCard PlayerCard
    {
        get
        {
            return _playerCard;
        }

        set
        {
            _playerCard = value;
        }
    }

    public Room CurrentRoom
    {
        get
        {
            return _currentRoom;
        }

        set
        {
            _currentRoom = value;
        }
    }

    // THIS INFO IS LOAD BY "GAME"
}

