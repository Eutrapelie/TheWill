using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [System.Serializable]
    public class LevelControllerData
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


        public LevelControllerData(LevelControllerData a_other)
        {
            _acteNumber = a_other.ActeNumber;
            _dayNumber = a_other.DayNumber;
            _characters = new List<CharacterInfo>();
            for (int i = 0; i < a_other.Characters.Count; i++)
            {
                _characters.Add(a_other.Characters[i]);
            }
            _kim = a_other.Kim;
            _startRoom = a_other.StartRoom;
        }

        public void ChangeRoomOfCharacter(Character a_character, Room a_room)
        {
            for (int i = 0; i < _characters.Count; i++)
            {
                if (_characters[i].characterName == a_character)
                {
                    _characters[i].currentRoom = a_room;
                }
            }
        }

        public void ChangeRoomSpotOfCharacter(Character a_character, RoomSpot a_roomSpot)
        {
            for (int i = 0; i < _characters.Count; i++)
            {
                if (_characters[i].characterName == a_character)
                {
                    _characters[i].currentRoomSpot = a_roomSpot;
                }
            }
        }
    }
}
