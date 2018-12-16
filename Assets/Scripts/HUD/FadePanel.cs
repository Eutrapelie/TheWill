using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheWill
{
    public class FadePanel : MonoBehaviour
    {
        [SerializeField] Text _roomTitleText;


        public void Ev_DisplayRoomName()
        {
            /*Color tempColor = _roomTitleText.color;
            tempColor.a = 1f;
            _roomTitleText.color = tempColor;
            _roomTitleText.text = string.IsNullOrEmpty(MainController.Instance.RoomName) ? SceneManager.GetActiveScene().name : MainController.Instance.RoomName;

            foreach (Room roomVisited in Game.Current.roomVisited)
            {
                if (roomVisited.ToString() == SceneManager.GetActiveScene().name)
                {
                    tempColor.a = 0f;
                    _roomTitleText.color = tempColor;
                    break;
                }
            }*/
        }
    }
}
