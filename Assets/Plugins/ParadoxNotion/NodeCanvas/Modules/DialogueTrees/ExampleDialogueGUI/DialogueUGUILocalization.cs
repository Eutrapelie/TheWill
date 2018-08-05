using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.DialogueTrees;

namespace NodeCanvas.DialogueTrees.UI.Examples{

	public class DialogueUGUILocalization : MonoBehaviour {

		[System.Serializable]
		public class SubtitleDelays
		{
			public float characterDelay = 0.05f;
			public float sentenceDelay  = 0.5f;
			public float commaDelay     = 0.1f;
			public float finalDelay     = 1.2f;
		}


		//Options...
		[Header("Input Options")]
		public bool skipOnInput;
		public bool waitForInput;
        List<KeyCode> _keyToIgnored = new List<KeyCode>();
        [SerializeField]
        Utils.Lang _lang;

		//Group...
		[Header("Subtitles")]
		public RectTransform subtitlesGroup;
		public Text actorSpeech;
		public Text actorName;
		public Image actorPortrait;
		public RectTransform waitInputIndicator;
		public SubtitleDelays subtitleDelays = new SubtitleDelays();

		//Group...
		[Header("Multiple Choice")]
		public RectTransform optionsGroup;
		public Button optionButton;
		private Dictionary<Button, int> cachedButtons;
		private Vector2 originalSubsPosition;
		private bool isWaitingChoice;

		private AudioSource _localSource;
		private AudioSource localSource{
			get {return _localSource != null? _localSource : _localSource = gameObject.AddComponent<AudioSource>();}
		}


		void OnEnable(){
			DialogueTree.OnDialogueStarted       += OnDialogueStarted;
			DialogueTree.OnDialoguePaused        += OnDialoguePaused;
			DialogueTree.OnDialogueFinished      += OnDialogueFinished;
			DialogueTree.OnSubtitlesRequest      += OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest += OnMultipleChoiceRequest;
		}

		void OnDisable(){
			DialogueTree.OnDialogueStarted       -= OnDialogueStarted;
			DialogueTree.OnDialoguePaused        -= OnDialoguePaused;
			DialogueTree.OnDialogueFinished      -= OnDialogueFinished;
			DialogueTree.OnSubtitlesRequest      -= OnSubtitlesRequest;
			DialogueTree.OnMultipleChoiceRequest -= OnMultipleChoiceRequest;
		}

		void Start(){
			subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
			optionButton.gameObject.SetActive(false);
			waitInputIndicator.gameObject.SetActive(false);
			originalSubsPosition = subtitlesGroup.transform.position;
            
            // test de localization
            Utils.Localization.InitializeLangDictionaries(_lang);

            _keyToIgnored.Add(KeyCode.Escape);
        }

		void OnDialogueStarted(DialogueTree dlg){
			//nothing special...
		}

		void OnDialoguePaused(DialogueTree dlg){
			subtitlesGroup.gameObject.SetActive(false);
			optionsGroup.gameObject.SetActive(false);
		}

		void OnDialogueFinished(DialogueTree dlg){
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


		void OnSubtitlesRequest(SubtitlesRequestInfo info){
			StartCoroutine(Internal_OnSubtitlesRequestInfo(info));
		}

        string tempColorText = string.Empty;
        string _tempBoldText = string.Empty;
        bool _isBold = false;

		IEnumerator Internal_OnSubtitlesRequestInfo(SubtitlesRequestInfo info){

            var id = info.statement.meta;
			var text = Utils.Localization.GetLocalized(id);
			var audio = info.statement.audio;
			var actor = info.actor;

			subtitlesGroup.gameObject.SetActive(true);
			actorSpeech.text = "";
			
			actorName.text = actor.name;
			actorSpeech.color = actor.dialogueColor;
			
			actorPortrait.gameObject.SetActive( actor.portraitSprite != null );
			actorPortrait.sprite = actor.portraitSprite;

			if (audio != null){
				var actorSource = actor.transform != null? actor.transform.GetComponent<AudioSource>() : null;
				var playSource = actorSource != null? actorSource : localSource;
				playSource.clip = audio;
				playSource.Play();
				actorSpeech.text = text;
				var timer = 0f;
				while (timer < audio.length)
                {
                    bool isKeyAllowed = false;
                    isKeyAllowed = !Input.GetKeyDown(_keyToIgnored[0]);
                    if (skipOnInput && Input.anyKeyDown && isKeyAllowed)
                    {
						playSource.Stop();
						break;
					}
					timer += Time.deltaTime;
					yield return null;
				}
			}

			if (audio == null){
				var tempText = "";
				var inputDown = false;
				if (skipOnInput){
					StartCoroutine(CheckInput( ()=>{ inputDown = true; } ));
				}

                bool isKeyAllowed = false;
                isKeyAllowed = !Input.GetKeyDown(_keyToIgnored[0]);
                for (int i= 0; i < text.Length; i++){

					if (skipOnInput && inputDown && isKeyAllowed)
                    {
						actorSpeech.text = text;
						yield return null;
						break;
					}

					if (subtitlesGroup.gameObject.activeSelf == false){
						yield break;
					}
                    
                    if (_isBold == false)
                    {
                        if (text[i] == '<' && text[i+1] == 'b' && text[i+2] == '>')
                        {
                            _isBold = true;
                            _tempBoldText = "<b>";
                            i += 2;
                        }
                    } else
                    {
                        _tempBoldText += text[i];
                        if (_tempBoldText.StartsWith("<b>") && _tempBoldText.Contains("</b>") == false)
                            {
                                // Write tempColorText with color
                            tempText += "<b>" + text[i].ToString() + "</b>";
                            yield return StartCoroutine(DelayPrint(subtitleDelays.characterDelay));
                            actorSpeech.text = tempText;
                        }
                        else if (_tempBoldText.StartsWith("<b>") && _tempBoldText.Contains("</b>"))
                        {
                            _tempBoldText = string.Empty;
                            _isBold = false;
                        }
                    }

                    if (string.IsNullOrEmpty(tempColorText))
                    {
                        if (text[i] == '<')
                        {
                            tempColorText = "<";
                        }
                        else
                        {
                            tempText += text[i];

                            yield return StartCoroutine(DelayPrint(subtitleDelays.characterDelay));
                            char c = text[i];
                            if (c == '.' || c == '!' || c == '?')
                            {
                                yield return StartCoroutine(DelayPrint(subtitleDelays.sentenceDelay));
                            }
                            if (c == ',')
                            {
                                yield return StartCoroutine(DelayPrint(subtitleDelays.commaDelay));
                            }

                            actorSpeech.text = tempText;
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

				if (!waitForInput)
                {
					yield return StartCoroutine(DelayPrint(subtitleDelays.finalDelay));
				}
			}

			if (waitForInput)
            {
				waitInputIndicator.gameObject.SetActive(true);

                bool isKeyAllowed = false;
                isKeyAllowed = !Input.GetKeyDown(_keyToIgnored[0]);
				while(!Input.anyKeyDown && !isKeyAllowed)
                {
					yield return null;
				}
				waitInputIndicator.gameObject.SetActive(false);
			}

			yield return null;
			subtitlesGroup.gameObject.SetActive(false);
			info.Continue();
		}

		IEnumerator CheckInput(System.Action Do){
			while(!Input.anyKeyDown){
				yield return null;
			}
			Do();
		}

		IEnumerator DelayPrint(float time){
			var timer = 0f;
			while (timer < time){
				timer += Time.deltaTime;
				yield return null;
			}
		}




		void OnMultipleChoiceRequest(MultipleChoiceRequestInfo info){

			optionsGroup.gameObject.SetActive(true);
			var buttonHeight = optionButton.GetComponent<RectTransform>().rect.height;
			optionsGroup.sizeDelta = new Vector2(optionsGroup.sizeDelta.x, (info.options.Values.Count * buttonHeight) + 20);

			cachedButtons = new Dictionary<Button, int>();
			int i = 0;

			foreach (KeyValuePair<IStatement, int> pair in info.options){
				var btn = (Button)Instantiate(optionButton);
				btn.gameObject.SetActive(true);
				btn.transform.SetParent(optionsGroup.transform, false);
				btn.transform.localPosition = (Vector2)optionButton.transform.localPosition - new Vector2(0, buttonHeight * i);
				btn.GetComponentInChildren<Text>().text = pair.Key.text;
				cachedButtons.Add(btn, pair.Value);
				btn.onClick.AddListener( ()=> { Finalize(info, cachedButtons[btn]);	});
				i++;
			}

			if (info.showLastStatement){
				subtitlesGroup.gameObject.SetActive(true);
				var newY = optionsGroup.position.y + optionsGroup.sizeDelta.y + 1;
				subtitlesGroup.position = new Vector2(subtitlesGroup.position.x, newY);
			}

			if (info.availableTime > 0){
				StartCoroutine(CountDown(info));
			}
		}

		IEnumerator CountDown(MultipleChoiceRequestInfo info){
			isWaitingChoice = true;
			var timer = 0f;
			while (timer < info.availableTime){
				if (isWaitingChoice == false){
					yield break;
				}
				timer += Time.deltaTime;
				SetMassAlpha(optionsGroup, Mathf.Lerp(1, 0, timer/info.availableTime));
				yield return null;
			}
			
			if (isWaitingChoice){
				Finalize(info, info.options.Values.Last());
			}
		}

		void Finalize(MultipleChoiceRequestInfo info, int index){
			isWaitingChoice = false;
			SetMassAlpha(optionsGroup, 1f);
			optionsGroup.gameObject.SetActive(false);
			if (info.showLastStatement){
				subtitlesGroup.gameObject.SetActive(false);
				subtitlesGroup.transform.position = originalSubsPosition;
			}
			foreach (var tempBtn in cachedButtons.Keys){
				Destroy(tempBtn.gameObject);
			}
			info.SelectOption(index);
		}

		void SetMassAlpha(RectTransform root, float alpha){
			foreach(var graphic in root.GetComponentsInChildren<CanvasRenderer>()){
				graphic.SetAlpha(alpha);
			}
		}
	}
}