using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{    
    public Character characterName;
    
    public Emotion currentEmotion;

    public Room currentRoom;

    public Gauge typeGauge;

    public int valueGauge;

    public CharacterInfo()
    {
        characterName = Character.Abigail;
        currentEmotion = Emotion.Neutral;
        currentRoom = Room.Hall;
        typeGauge = Gauge.Courtesy;
        valueGauge = 0;
    }

    public CharacterInfo(Character character, Emotion emotion, Room room, Gauge gauge, int vg)
    {
        characterName = character;
        currentEmotion = emotion;
        currentRoom = room;
        typeGauge = gauge;
        valueGauge = vg;
    }

    public CharacterInfo(CharacterInfo cI)
    {
        characterName = cI.characterName;
        currentEmotion = cI.currentEmotion;
        currentRoom = cI.currentRoom;
        typeGauge = cI.typeGauge;
        valueGauge = cI.valueGauge;
    }
}
