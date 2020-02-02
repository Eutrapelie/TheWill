using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [Serializable]
    public struct LevelData
    {
        public string name;
        public int acteNumber;
        public int dayNumber;
        public Room startRoom;
        public Player kim;
        public List<CharacterInfo> characters;
    }
}
