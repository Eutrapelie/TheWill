using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;

namespace NodeCanvas.DialogueTrees.UI.Examples
{
	public class DialogueUGUILocalization : MonoBehaviour
    {
        const string NARRATOR_NAME = " ";


		[System.Serializable]
		public class SubtitleDelays
		{
			public float characterDelay = 0.05f;
			public float sentenceDelay  = 0.5f;
			public float commaDelay     = 0.1f;
			public float finalDelay     = 1.2f;
		}

        
		[Header("Input Options")]
		public bool skipOnInput;
		public bool waitForInput;
        public bool isGamePaused;
        List<KeyCode> _keysToIgnored = new List<KeyCode>();

        [Header("Localization")]
        /*[Range(0, 1)]
        [SerializeField] int _minPathIndex;
        [Range(0, 1)]
        [SerializeField] int _maxPathIndex;*/
        [SerializeField] Utils.Lang _lang;
        public void SetLang(Utils.Lang a_lang) {
            _lang = a_lang;
            Utils.Localization.InitializeLangDictionaries(_lang);
        }
        
		[Header("Subtitles")]
		public RectTransform subtitlesGroup;
		public Text actorSpeech;
		public Text actorName;
		public Image actorPortrait;
		public RectTransform waitInputIndicator;
		public SubtitleDelays subtitleDelays = new SubtitleDelays();
        
		[Header("Multiple Choice")]
		public RectTransform optionsGroup;
		public Button optionButton;
		Dictionary<Button, int> cachedButtons;
		Vector2 originalSubsPosition;
		bool isWaitingChoice;

		AudioSource _localSource;
		AudioSource localSource
        {
			get {return _localSource != null? _localSource : _localSource = gameObject.AddComponent<AudioSource>();}
        }

        string tempColorText = string.Empty;
        string _tempBoldText = string.Empty;
        bool _isBold = false;


        void OnEnable()
        {
			DialogueTree.OnDialogueStarted       += OnDialogueStarted;
			DialogueTree.OnDialoguePaused        += OnDialoguePaused;
			DialogueTree.OnDialogueFinished      += OnDialogueFinished;
			DialogueTree.OnSubtitlesRequest      += OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest += OnMultipleChoiceRequest;
        }
        /*********************************************************/

        void OnDisable()
        {
			DialogueTree.OnDialogueStarted       -= OnDialogueStarted;
			DialogueTree.OnDialoguePaused        -= OnDialoguePaused;
			DialogueTree.OnDialogueFinished      -= OnDialogueFinished;
			DialogueTree.OnSubtitlesRequest      -= OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest -= OnMultipleChoiceRequest;
        }
        /*********************************************************/

        void Start()
        {
            subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
			optionButton.gameObject.SetActive(false);
			waitInputIndicator.gameObject.SetActive(false);
			originalSubsPosition = subtitlesGroup.transform.position;

            _keysToIgnored.Add(KeyCode.Escape);
        }
        /*********************************************************/

        void OnDialogueStarted(DialogueTree a_dlg)
        {
            //nothing special...
        }
        /*********************************************************/

        void OnDialoguePaused(DialogueTree a_dlg)
        {
			subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
        }
        /*********************************************************/

        void OnDialogueFinished(DialogueTree a_dlg)
        {
			subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
			if (cachedButtons != null){
				foreach (var tempBtn in cachedButtons.Keys){
					if (tempBtn != null){
						Destroy(tempBtn.gameObject);
					}
				}
				cachedButtons = null;
			}
        }
        /*********************************************************/

        void OnSubtitlesRequest(SubtitlesRequestInfo a_info)
        {
			StartCoroutine(Internal_OnSubtitlesRequestInfo(a_info));
        }
        /*********************************************************/

        string HexaColor(Color a_color)
        {
            return "#" + ColorUtility.ToHtmlStringRGBA(a_color);
        }
        /*********************************************************/

        IEnumerator Internal_OnSubtitlesRequestInfo(SubtitlesRequestInfo a_info)
        {            
			var text = a_info.statement.text;
            _isBold = false;
            _tempBoldText = string.Empty;
			var audio = a_info.statement.audio;
			var actor = a_info.actor;

			subtitlesGroup.gameObject.SetActive(true);
			actorSpeech.text = string.Empty;
			
			actorName.text = actor.name;
            actorName.color = actor.dialogueColor;
            Debug.Log(HexaColor(actorName.color));
			//actorSpeech.color = actor.dialogueColor;
			
			actorPortrait.gameObject.SetActive( actor.portraitSprite != null );
			actorPortrait.sprite = actor.portraitSprite;

			if (audio != null)
            {
				var actorSource = actor.transform != null? actor.transform.GetComponent<AudioSource>() : null;
				var playSource = actorSource != null? actorSource : localSource;
				playSource.clip = audio;
				playSource.Play();
                if (actor.name == NARRATOR_NAME)
                    actorSpeech.text = "<i>" + text + "</i>";
                else
                    actorSpeech.text = text;
				var timer = 0f;
				while (timer < audio.length)
                {
                    bool isKeyAllowed = false;
                    isKeyAllowed = !Input.GetKeyDown(_keysToIgnored[0]);
                    if (!isGamePaused && skipOnInput && Input.anyKeyDown && isKeyAllowed)
                    {
						playSource.Stop();
						break;
					}
					timer += Time.deltaTime;
					yield return null;
				}
			}

			if (audio == null)
            {
				var tempText = string.Empty;
				var inputDown = false;
                if (!isGamePaused && skipOnInput)
                {
                    StartCoroutine(CheckInput(() => { inputDown = true; }));
                }

                bool isKeyAllowed = false;
                isKeyAllowed = !Input.GetKeyDown(_keysToIgnored[0]);

                for (int i= 0; i < text.Length; i++)
                {
                    //Debug.Log(i + "] " + text[i] + "] " + tempText + " -- isBold: " + _isBold + " (" + _tempBoldText + ")");
					if (!isGamePaused && skipOnInput && inputDown && isKeyAllowed) // Skip
                    {
                        if (actor.name == NARRATOR_NAME)
                            actorSpeech.text = "<i>" + text + "</i>";
                        else
                            actorSpeech.text = text.Replace("<b>", "<b><color=" +HexaColor(actorName.color) + ">").Replace("</b>", "</color></b>");
						yield return null;
						break;
					}

					if (subtitlesGroup.gameObject.activeSelf == false){
						yield break;
					}

                    if (_isBold == false) // Bold treatment
                    {
                        if (text[i] == '<' && text[i+1] == 'b' && text[i+2] == '>')
                        {
                            _isBold = true;
                            _tempBoldText = "<b><color=" + HexaColor(actorName.color) + ">";
                            i += 2;
                            continue;
                        }
                    }
                    else
                    {
                        _tempBoldText += text[i];
                        if (_tempBoldText.StartsWith("<b>") && text[i] == '<' && text[i + 1] == '/' && text[i + 2] == 'b' && text[i + 3] == '>')
                        {
                            _tempBoldText = string.Empty;
                            _isBold = false;
                            i += 3;
                            continue;
                        }
                        else if (_tempBoldText.StartsWith("<b>") && _tempBoldText.Contains("</b>") == false)
                        {
                            // Write tempColorText with color
                            tempText += "<b><color=" + HexaColor(actorName.color) + ">" + text[i].ToString() + "</color></b>";
                            yield return StartCoroutine(DelayPrint(subtitleDelays.characterDelay));
                            if (actor.name == NARRATOR_NAME)
                                actorSpeech.text = "<i>" + tempText + "</i>";
                            else
                                actorSpeech.text = tempText;
                            continue;
                        }
                        /*else if (_tempBoldText.StartsWith("<b>") && _tempBoldText.Contains("</b>"))
                        {
                            _tempBoldText = string.Empty;
                            _isBold = false;
                        }*/
                    }

                    if (string.IsNullOrEmpty(tempColorText)) // Color treatment
                    {
                        if (text[i] == '<')
                        {
                            tempColorText = "<";
                        }
                        else
                        {
                            tempText += text[i];

                            yield return StartCoroutine(DelayPrint(subtitleDelays.characterDelay));
                            if (actor.name == NARRATOR_NAME)
                                actorSpeech.text = "<i>" + tempText + "</i>";
                            else
                                actorSpeech.text = tempText;
                            char c = text[i];
                            if (c == '.' || c == '!' || c == '?')
                            {
                                yield return StartCoroutine(DelayPrint(subtitleDelays.sentenceDelay));
                            }
                            if (c == ',')
                            {
                                yield return StartCoroutine(DelayPrint(subtitleDelays.commaDelay));
                            }
                        }
                    }
                    else 
                    {
                        tempColorText += text[i];
                        if (tempColorText.StartsWith("<color=") && tempColorText.Contains(">") && tempColorText.Contains("</") == false)
                        {
                            if (text[i] != '<' && text[i] != '/' && text[i] != '>')
                            {
                                // Write tempColorText with color
                                tempText += "<color=red>" + text[i].ToString() + "</color>";
                                yield return StartCoroutine(DelayPrint(subtitleDelays.characterDelay));
                                if (actor.name == NARRATOR_NAME)
                                    actorSpeech.text = "<i>" + tempText + "</i>";
                                else
                                    actorSpeech.text = tempText;
                            }
                        }
                        else if (tempColorText.StartsWith("<color=") && tempColorText.EndsWith("</color>"))
                        {
                            // Reset tempColorText
                            tempColorText = string.Empty;
                        }
                    }
				}

				if (!isGamePaused && !waitForInput)
                {
					yield return StartCoroutine(DelayPrint(subtitleDelays.finalDelay));
				}
			}

			if (waitForInput)
            {
				waitInputIndicator.gameObject.SetActive(true);

                bool isKeyAllowed = true;
                foreach (KeyCode key in _keysToIgnored)
                    isKeyAllowed &= !Input.GetKey(key);

                while (isGamePaused || Input.anyKeyDown == false || isKeyAllowed == false)
                {
                    yield return null;
                    isKeyAllowed = true;
                    foreach (KeyCode key in _keysToIgnored)
                        isKeyAllowed &= !Input.GetKey(key);
                }
                waitInputIndicator.gameObject.SetActive(false);
			}

			yield return null;
			subtitlesGroup.gameObject.SetActive(false);
			a_info.Continue();
        }
        /*********************************************************/

        IEnumerator CheckInput(System.Action a_action)
        {
			while(!Input.anyKeyDown)
            {
				yield return null;
			}
			a_action();
        }
        /*********************************************************/

        IEnumerator DelayPrint(float a_time)
        {
			var timer = 0f;
			while (timer < a_time)
            {
				timer += Time.deltaTime;
				yield return null;
			}
        }
        /*********************************************************/

        void OnMultipleChoiceRequest(MultipleChoiceRequestInfo a_info)
        {
			optionsGroup.gameObject.SetActive(true);
			var buttonHeight = optionButton.GetComponent<RectTransform>().rect.height;
			optionsGroup.sizeDelta = new Vector2(optionsGroup.sizeDelta.x, (a_info.options.Values.Count * buttonHeight) + 20);

			cachedButtons = new Dictionary<Button, int>();
			int i = 0;

			foreach (KeyValuePair<IStatement, int> pair in a_info.options)
            {
				var btn = (Button)Instantiate(optionButton);
				btn.gameObject.SetActive(true);
				btn.transform.SetParent(optionsGroup.transform, false);
				btn.transform.localPosition = (Vector2)optionButton.transform.localPosition - new Vector2(0, buttonHeight * i);
                
				btn.GetComponentInChildren<Text>().text = pair.Key.text;
				cachedButtons.Add(btn, pair.Value);
				btn.onClick.AddListener( ()=> { Finalize(a_info, cachedButtons[btn]);	});
				i++;
			}

			if (a_info.showLastStatement){
				subtitlesGroup.gameObject.SetActive(true);
				var newY = optionsGroup.position.y + optionsGroup.sizeDelta.y + 1;
				subtitlesGroup.position = new Vector2(subtitlesGroup.position.x, newY);
			}

			if (a_info.availableTime > 0){
				StartCoroutine(CountDown(a_info));
			}
        }
        /*********************************************************/

        IEnumerator CountDown(MultipleChoiceRequestInfo a_info)
        {
			isWaitingChoice = true;
			var timer = 0f;
			while (timer < a_info.availableTime){
				if (isWaitingChoice == false){
					yield break;
				}
				timer += Time.deltaTime;
				SetMassAlpha(optionsGroup, Mathf.Lerp(1, 0, timer/a_info.availableTime));
				yield return null;
			}
			
			if (isWaitingChoice){
				Finalize(a_info, a_info.options.Values.Last());
			}
        }
        /*********************************************************/

        void Finalize(MultipleChoiceRequestInfo a_info, int a_index)
        {
			isWaitingChoice = false;
			SetMassAlpha(optionsGroup, 1f);
			optionsGroup.gameObject.SetActive(false);
			if (a_info.showLastStatement){
				subtitlesGroup.gameObject.SetActive(false);
				subtitlesGroup.transform.position = originalSubsPosition;
			}
			foreach (var tempBtn in cachedButtons.Keys){
				Destroy(tempBtn.gameObject);
			}
			a_info.SelectOption(a_index);
        }
        /*********************************************************/

        void SetMassAlpha(RectTransform a_root, float a_alpha)
        {
			foreach(var graphic in a_root.GetComponentsInChildren<CanvasRenderer>())
            {
				graphic.SetAlpha(a_alpha);
			}
        }
        /*********************************************************/
    }
}