using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheWill
{
    public class GoToSceneButton : MonoBehaviour
    {
        [SerializeField] Room _room;


        void Start()
        {
            Button button = this.gameObject.GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(() => GoToRoom(_room));
            }
        }

        void GoToRoom(Room room)
        {
            Game.Current.SaveGameFromManager();
            SceneManager.LoadScene(room.ToString());
        }
    }
}
