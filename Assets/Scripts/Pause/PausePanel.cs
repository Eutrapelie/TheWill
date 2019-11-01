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
        public bool IsPanelShow() { return _isPanelShow; }
        //1/DialogueUGUI _dialogueObject;
        DialogueUGUILocalization _dialogueObject;

        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            Instance = this;
        }
        /*********************************************************/

        void Start()
        {
            Color colorTemp = _infoText.color;
            colorTemp.a = 0f;
            _infoText.color = colorTemp;
            SceneChanged(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
            SceneManager.activeSceneChanged += SceneChanged;

            ChangeVisibility(false);
        }
        /*********************************************************/

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (_dialogueObject == null)
                    //1/_dialogueObject = FindObjectOfType<DialogueUGUI>();
                    _dialogueObject = FindObjectOfType<DialogueUGUILocalization>();
                ChangeVisibility(!_isPanelShow);
            }
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void ChangeVisibility(bool a_show)
        {
            _isPanelShow = a_show;
            _canvasGroupParent.alpha = a_show ? 1 : 0;
            _canvasGroupParent.interactable = a_show;
            _canvasGroupParent.blocksRaycasts = a_show;
            _dialogueObject.isGamePaused = _isPanelShow;
            GameManager.Instance.allowClickOnObject = !a_show;
        }
        /*********************************************************/

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
        /*********************************************************/

        void SceneChanged(Scene a_s1, Scene a_s2)
        {
            Debug.Log("SceneChanged");
            foreach(GameObject go in a_s2.GetRootGameObjects())
            {
                if (go.tag == "DialogueUGUI")
                {
                    //1/_dialogueObject = go.GetComponent<DialogueUGUI>();
                    _dialogueObject = go.GetComponent<DialogueUGUILocalization>();
                    break;
                }
            }

            _dialogueObject.SetLang(Options.Current.GetLang());
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
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void Btn_Continue()
        {
            if (_dialogueObject == null)
                //1/_dialogueObject = FindObjectOfType<DialogueUGUI>();
                _dialogueObject = FindObjectOfType<DialogueUGUILocalization>();
            ChangeVisibility(false);
        }
        /*********************************************************/

        public void UpdateDisplayAfterSave()
        {
            ChangeVisibility(false);
            StartCoroutine(DisplayInfoMessage("Sauvegarde effectuée."));
        }
        /*********************************************************/

        public void Btn_Load()
        {

        }
        /*********************************************************/

        public void Btn_Quit()
        {
            SceneManager.LoadScene("Menu");
            GameManager.DestroyInstance();
            //MusicManager.Instance.ResumeToMenu();
            //Application.Quit();
        }
        /*********************************************************/
    }
}
