using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelController", menuName = "InGameControllers/LevelController", order = 1)]
public class LevelController : ScriptableObject
{
    [SerializeField]
    private int _acteNumber;

    [SerializeField]
    private int _dayNumber;

    [SerializeField]
    private Room _startRoom;

    [SerializeField]
    private Player _kim;

    [SerializeField]
    private List<CharacterInfo> _characters;    

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
}
