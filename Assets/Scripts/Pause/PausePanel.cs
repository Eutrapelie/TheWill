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
            SceneChanged(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
            SceneManager.activeSceneChanged += SceneChanged;
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

        void SceneChanged(Scene a_s1, Scene a_s2)
        {
            foreach(GameObject go in a_s2.GetRootGameObjects())
            {
                if (go.tag == "DialogueUGUI")
                {
                    _dialogueObject = go.GetComponent<DialogueUGUI>();
                    break;
                }
            }

            _dialogueObject.subtitleDelays.characterDelay = Options.Current.GetCharacterDelay();
            _dialogueObject.subtitleDelays.sentenceDelay = Options.Current.GetSentenceDelay();
            _dialogueObject.subtitleDelays.commaDelay = Options.Current.GetCommaDelay();
            _dialogueObject.subtitleDelays.finalDelay = Options.Current.GetFinalDelay();

            foreach (Text text in _dialogueObject.GetComponentsInChildren<Text>(true))
            {
                //Debug.Log(text.name);
                text.fontSize = Options.Current.GetFontSizeInPixels();
                if (text.name == "Name")
                    text.fontSize += 2;
            }
        }

        public void Btn_Continue()
        {
            ChangeVisibility(false);
            if (_dialogueObject == null)
                _dialogueObject = FindObjectOfType<DialogueUGUI>();
            _dialogueObject.isGamePaused = _isPanelShow;
        }

        public void UpdateDisplayAfterSave()
        {
            ChangeVisibility(false);
            StartCoroutine(DisplayInfoMessage("Sauvegarde effectuée."));
        }

        public void Btn_Load()
        {

        }

        public void Btn_Quit()
        {
            SceneManager.LoadScene("Menu");
            //Application.Quit();
        }
    }
}
