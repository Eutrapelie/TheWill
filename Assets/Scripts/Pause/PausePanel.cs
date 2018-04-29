using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NodeCanvas.DialogueTrees.UI.Examples;

namespace TheWill
{
    public class PausePanel : MonoBehaviour
    {
        public static PausePanel Instance;

        [SerializeField] CanvasGroup _canvasGroupParent;
        [SerializeField] Text _infoText;

        bool _isPanelShow;
        DialogueUGUI _dialogueObject;


        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ChangeVisibility(false);

            Color colorTemp = _infoText.color;
            colorTemp.a = 0f;
            _infoText.color = colorTemp;

            SceneManager.activeSceneChanged += (s1, s2) => { _dialogueObject = FindObjectOfType<DialogueUGUI>(); };
        }
        
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ChangeVisibility(!_isPanelShow);
                if (_dialogueObject == null)
                    _dialogueObject = FindObjectOfType<DialogueUGUI>();
                _dialogueObject.isGamePaused = _isPanelShow;
            }
        }

        void ChangeVisibility(bool a_show)
        {
            _isPanelShow = a_show;
            _canvasGroupParent.alpha = a_show ? 1 : 0;
            _canvasGroupParent.interactable = a_show;
            _canvasGroupParent.blocksRaycasts = a_show;
        }

        IEnumerator DisplayInfoMessage(string a_message)
        {
            Color colorTemp = _infoText.color;
            colorTemp.a = 1f;
            _infoText.color = colorTemp;
            _infoText.text = a_message;
            yield return new WaitForSeconds(Constants.INFO_MESSAGE_DURATION);
            colorTemp.a = 0f;
            _infoText.color = colorTemp;
        }

        public void Btn_Continue()
        {
            ChangeVisibility(false);
            if (_dialogueObject == null)
                _dialogueObject = FindObjectOfType<DialogueUGUI>();
            _dialogueObject.isGamePaused = _isPanelShow;
        }

        public void Btn_Save()
        {
            SaveLoad.Save();
            ChangeVisibility(false);
            StartCoroutine(DisplayInfoMessage("Sauvegarde effectuée."));
        }

        public void Btn_Load()
        {

        }

        public void Btn_Quit()
        {
            Application.Quit();
        }
    }
}
