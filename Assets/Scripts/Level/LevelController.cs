using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelController", menuName = "InGameControllers/LevelController", order = 1)]
public class LevelController : ScriptableObject
{
    [SerializeField] int _acteNumber;
    public int ActeNumber
    {
        get
        {
            return _acteNumber;
        }

        set
        {
            _acteNumber = value;
        }
    }
    [SerializeField] int _dayNumber;
    public int DayNumber
    {
        get
        {
            return _dayNumber;
        }

        set
        {
            _dayNumber = value;
        }
    }

    [SerializeField] Room _startRoom;
    public Room StartRoom
    {
        get
        {
            return _startRoom;
        }

        set
        {
            _startRoom = value;
        }
    }
    [SerializeField] Player _kim;
    public Player Kim
    {
        get
        {
            return _kim;
        }

        set
        {
            _kim = value;
        }
    }

    [SerializeField] List<CharacterInfo> _characters;    
    public List<CharacterInfo> Characters
    {
        get
        {
            return _characters;
        }

        set
        {
            _characters = value;
        }
    }
}
