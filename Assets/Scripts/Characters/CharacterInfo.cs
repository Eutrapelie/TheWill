using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public CharacterInfo(Character character, Emotion emotion, Room room, RoomSpot roomSpot, Gauge gauge, int vg)
    {
        characterName = character;
        currentEmotion = emotion;
        currentRoom = room;
        currentRoomSpot = roomSpot;
        typeGauge = gauge;
        valueGauge = vg;
    }

    public CharacterInfo(CharacterInfo cI)
    {
        characterName = cI.characterName;
        currentEmotion = cI.currentEmotion;
        currentRoom = cI.currentRoom;
        currentRoomSpot = cI.currentRoomSpot;
        typeGauge = cI.typeGauge;
        valueGauge = cI.valueGauge;
    }

    public override string ToString()
    {
        return characterName + " [" + currentEmotion + "] in " + currentRoom + " (" + currentRoomSpot + ")";
    }
}
