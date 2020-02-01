using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheWill
{
    public class RoomSelectionButton : MonoBehaviour
    {
        [SerializeField] Text _nameText;
        Button _button;
        Sprite _roomSprite;
        Sprite _lockRoomSprite;
        string _name;
        bool _isRoomKnown;
        bool _isRoomAccessible;


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Start()
        {
            _name = _nameText.text;
            EventManager.StartListening(RoomsChoice.EVT_ISKNOWN_ROOM, SetIsRoomKnown);
        }
        /*********************************************************/

        void OnDestroy()
        {
            EventManager.StopListening(RoomsChoice.EVT_ISKNOWN_ROOM, SetIsRoomKnown);
        }
        /*********************************************************/

    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void Init(Sprite a_lockRoomSprite)
        {
            _button = GetComponent<Button>();
            _lockRoomSprite = a_lockRoomSprite;
            Deactivate();
        }
        /*********************************************************/

        void Deactivate()
        {
            _button.image.sprite = _lockRoomSprite;
            _nameText.text = "???";
            _button.interactable = false;
            _isRoomAccessible = false;
            _isRoomKnown = false;
        }
        /*********************************************************/

        /*public void SetIsRoomKnown(bool a_isKnown)
        {
            _isRoomKnown = a_isKnown;
        }
        /*********************************************************/

        void SetIsRoomKnown(object args)
        {
            List<object> list = (List<object>)args;
            Room arg0 = (Room)list[0];
            bool arg1 = (bool)list[1];
            Debug.Log("\tRoom: " + arg0.ToString() + " - IsRoomKnown: " + arg1);
            /*foreach (CharacterCard item in Characters)
            {
                if (item.CharacterInfo.characterName.Equals(arg0))
                {
                    item.ChangeCurrentEmotion(arg1);
                    break;
                }
            }*/
        }
        /*********************************************************/
    }
}
