using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelController", menuName = "InGameControllers/LevelController", order = 1)]
public class LevelController : ScriptableObject
{
    [SerializeField]
    private int _levelNumber;

    [SerializeField]
    private Room _room;

    [SerializeField]
    private List<Character> _charactersInvolved;

    

    public int LevelNumber
    {
        get
        {
            return _levelNumber;
        }

        set
        {
            _levelNumber = value;
        }
    }

    public Room Room
    {
        get
        {
            return _room;
        }

        set
        {
            _room = value;
        }
    }

    public List<Character> CharactersInvolved
    {
        get
        {
            return _charactersInvolved;
        }

        set
        {
            _charactersInvolved = value;
        }
    }
}
