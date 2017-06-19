using NodeCanvas.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterTalkingEvent : UnityEvent<Character>
{
}

[System.Serializable]
public class CharacterTogglingEvent : UnityEvent<Character, bool>
{
}

[System.Serializable]
public class CharacterChangeRoomEvent : UnityEvent<Character, Room>
{
}

[System.Serializable]
public class CharacterChangeRoomSpotEvent : UnityEvent<Character, RoomSpot>
{
}

[System.Serializable]
public class CharacterChangeEmotionEvent : UnityEvent<Character, Emotion>
{
}

[System.Serializable]
public class CharacterSetGaugeEvent : UnityEvent<Character, int>
{
}


public class MyCharacterController : MonoBehaviour
{
    [SerializeField]
    private List<CharacterCard> _characters;

    [SerializeField]
    private GlobalBlackboard _globalBlackboard;

    public CharacterTogglingEvent characterTogglingEvent;

    public CharacterTalkingEvent characterTalkingEvent;

    public CharacterChangeRoomEvent characterChangeRoomEvent;

    public CharacterChangeRoomSpotEvent characterChangeRoomSpotEvent;

    public CharacterChangeEmotionEvent characterChangeEmotionEvent;

    public CharacterSetGaugeEvent characterSetGaugeEvent;

    private CharacterCard talkingCharacter;

    public List<CharacterCard> Characters
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

    void Awake()
    {
        talkingCharacter = null;
        if (characterTalkingEvent == null)
        {
            characterTalkingEvent = new CharacterTalkingEvent();
        }
        characterTalkingEvent.AddListener(CharacterIsTalking);

        if(characterTogglingEvent == null)
        {
            characterTogglingEvent = new CharacterTogglingEvent();
        }
        characterTogglingEvent.AddListener(CharacterApparition);

        if (characterChangeRoomEvent == null)
        {
            characterChangeRoomEvent = new CharacterChangeRoomEvent();
        }
        characterChangeRoomEvent.AddListener(CharacterChangeRoom);

        if (characterChangeRoomSpotEvent == null)
        {
            characterChangeRoomSpotEvent = new CharacterChangeRoomSpotEvent();
        }
        characterChangeRoomSpotEvent.AddListener(CharacterChangeRoomSpot);

        if (characterChangeEmotionEvent == null)
        {
            characterChangeEmotionEvent = new CharacterChangeEmotionEvent();
        }
        characterChangeEmotionEvent.AddListener(CharacterChangeEmotion);

        if (characterSetGaugeEvent == null)
        {
            characterSetGaugeEvent = new CharacterSetGaugeEvent();
        }
        characterSetGaugeEvent.AddListener(CharacterSetGauge);
    }

    public void AddCharacterCardToGlobalBB(CharacterCard card)
    {
        Variable found = _globalBlackboard.GetVariable(card.CharacterInfo.characterName + "Card");
        if (found != null)
        {
            found.value = card;
        }
    }

    private void CharacterIsTalking(Character arg0)
    {
        if(talkingCharacter != null && talkingCharacter.CharacterInfo.characterName.Equals(arg0))
        {
            return;
        }

        foreach (CharacterCard item in Characters)
        {
            if (item.IsTalking && !item.CharacterInfo.characterName.Equals(arg0))
            {
                item.IsTalking = false;
                Debug.Log("[MyCharacterController] " + item.CharacterInfo.characterName + " stops talking");
                talkingCharacter = item;
                item.ChangeSprite();
            }
            else if (!item.IsTalking && item.CharacterInfo.characterName.Equals(arg0))
            {
                item.IsTalking = true;
                Debug.Log("[MyCharacterController] " + item.CharacterInfo.characterName + " is talking");
                talkingCharacter = item;
                item.ChangeSprite();
            }
        }
    }

    private void CharacterApparition(Character character, bool visible)
    {
        foreach (CharacterCard item in Characters)
        {
            if(item.CharacterInfo.characterName.Equals(character))
            {
                item.ToggleSprite(visible);
            }
        }
    }

    private void CharacterChangeRoom(Character arg0, Room arg1)
    {
        foreach (CharacterCard item in Characters)
        {
            if (item.CharacterInfo.characterName.Equals(arg0))
            {
                item.ChangeRoom(arg1);
                break;
            }
        }
    }

    private void CharacterChangeRoomSpot(Character arg0, RoomSpot arg1)
    {
        foreach (CharacterCard item in Characters)
        {
            if (item.CharacterInfo.characterName.Equals(arg0))
            {
                item.ChangeRoomSpot(arg1);
                break;
            }
        }
    }

    private void CharacterChangeEmotion(Character arg0, Emotion arg1)
    {
        foreach (CharacterCard item in Characters)
        {
            if (item.CharacterInfo.characterName.Equals(arg0))
            {
                item.ChangeCurrentEmotion(arg1);
                break;
            }
        }
    }

    private void CharacterSetGauge(Character arg0, int arg1)
    {
        foreach (CharacterCard item in Characters)
        {
            if (item.CharacterInfo.characterName.Equals(arg0))
            {
                item.SetGauge(arg1);
                break;
            }
        }
    }

}
