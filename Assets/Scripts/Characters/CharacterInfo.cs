using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWill
{
    [System.Serializable]
    public class CharacterInfo
    {
        public Character characterName;
        public Emotion currentEmotion;
        public Room currentRoom;
        public RoomSpot currentRoomSpot;
        public Gauge typeGauge;
        public int valueGauge;


        public CharacterInfo()
        {
            characterName = Character.Abigail;
            currentEmotion = Emotion.Neutral;
            currentRoom = Room.Hall;
            currentRoomSpot = RoomSpot.LivingRoomSpot1;
            typeGauge = Gauge.Courtesy;
            valueGauge = 0;
        }

        public CharacterInfo(Character a_character, Emotion a_emotion, Room a_room, RoomSpot a_roomSpot, Gauge a_gauge, int a_vg)
        {
            characterName = a_character;
            currentEmotion = a_emotion;
            currentRoom = a_room;
            currentRoomSpot = a_roomSpot;
            typeGauge = a_gauge;
            valueGauge = a_vg;
        }

        public CharacterInfo(CharacterInfo a_characterInfo)
        {
            characterName = a_characterInfo.characterName;
            currentEmotion = a_characterInfo.currentEmotion;
            currentRoom = a_characterInfo.currentRoom;
            currentRoomSpot = a_characterInfo.currentRoomSpot;
            typeGauge = a_characterInfo.typeGauge;
            valueGauge = a_characterInfo.valueGauge;
        }

        public override string ToString()
        {
            return characterName + " [" + currentEmotion + "] in " + currentRoom + " (" + currentRoomSpot + ")";
        }
    }
}
