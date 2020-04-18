using System;
using UnityEngine;

namespace TheWill
{
    [Serializable]
    [CreateAssetMenu(fileName = "RoomsData", menuName = "InGameControllers/RoomsData", order = 2)]
    public class RoomsData : ScriptableObject
    {
        public RoomData[] rooms;
    }

    [Serializable]
    public class RoomData
    {
        public string name;
        public Sprite miniature;
    }
}
