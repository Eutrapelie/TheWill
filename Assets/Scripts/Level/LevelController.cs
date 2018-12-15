using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [CreateAssetMenu(fileName = "LevelController", menuName = "InGameControllers/LevelController", order = 1)]
    [Serializable]
    public class LevelController : ScriptableObject
    {
        [SerializeField] LevelControllerData _levelControllerData;
        public LevelControllerData Data { get { return _levelControllerData; }
        }
        /*[SerializeField] int _acteNumber;
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
        }*/

        public LevelController(LevelController a_other)
        {
            //CreateInstance("newLevel");
            _levelControllerData = new LevelControllerData(a_other.Data);
        }
    } // Class
} // Namespace
