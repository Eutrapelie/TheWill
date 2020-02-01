using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheWill
{
    public class RoomSelectionButton : MonoBehaviour
    {
        [SerializeField] Text _nameText;
        [SerializeField] Room _room;
        Button _button;
        Sprite _roomSprite;
        Sprite _lockRoomSprite;
        string _name;
        bool _isRoomKnown;
        bool _isRoomAccessible;

        const string UNKNOWN_ROOM_NAME = "???";


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Start()
        {
            EventManager.StartListening(RoomsChoice.EVT_ISKNOWN_ROOM, SetRoomKnown);
            EventManager.StartListening(RoomsChoice.EVT_ISACCESSIBLE_ROOM, SetRoomAccessible);
        }
        /*********************************************************/

        void OnDestroy()
        {
            EventManager.StopListening(RoomsChoice.EVT_ISKNOWN_ROOM, SetRoomKnown);
            EventManager.StopListening(RoomsChoice.EVT_ISACCESSIBLE_ROOM, SetRoomAccessible);
        }
        /*********************************************************/

    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void Init(Sprite a_lockRoomSprite)
        {
            _button = GetComponent<Button>();
            _name = _nameText.text;
            _roomSprite = _button.image.sprite;
            _lockRoomSprite = a_lockRoomSprite;
            Deactivate();
        }
        /*********************************************************/

        void Deactivate()
        {
            _button.image.sprite = _lockRoomSprite;
            _nameText.text = UNKNOWN_ROOM_NAME;
            _button.interactable = false;
            _isRoomAccessible = false;
            _isRoomKnown = false;
        }
        /*********************************************************/

        void SetRoomKnown(object a_args)
        {
            List<object> list = (List<object>)a_args;
            Room arg0 = (Room)list[0];

            if (_room.Equals(arg0))
            {
                _isRoomKnown = (bool)list[1];
                Debug.Log("\tRoom: " + _room.ToString() + " - IsRoomKnown: " + _isRoomKnown);
                _nameText.text = _isRoomKnown ? _name : UNKNOWN_ROOM_NAME;
            }
        }
        /*********************************************************/

        void SetRoomAccessible(object a_args)
        {
            List<object> list = (List<object>)a_args;
            Room arg0 = (Room)list[0];

            if (_room.Equals(arg0))
            {
                _isRoomAccessible = (bool)list[1];
                Debug.Log("\tRoom: " + _room.ToString() + " - IsRoomAccessible: " + _isRoomAccessible);
                _nameText.text = _name; // Even if room is not accessible, its name cannot change to UNKNOWN_ROOM_NAME
                _button.image.sprite = _isRoomAccessible ? _roomSprite : _lockRoomSprite;
                _button.interactable = _isRoomAccessible;
            }
        }
        /*********************************************************/
    }
}
