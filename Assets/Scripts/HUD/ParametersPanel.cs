using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheWill
{
    public enum Language { French, English }
    public enum Resolution { _1920_1080, _1280_720 }
    public enum FontSize { Petite, Standard, Grande }
    public enum ReadingSpeed { Lente, Standard, Rapide }


    [RequireComponent(typeof(Animator))]
    public class ParametersPanel : MonoBehaviour
    {
        [SerializeField] Slider _volumeSlider;
        [SerializeField] Text _volumeValueText;
        [SerializeField] ParameterButtonsElement _languageElement;
        [SerializeField] ParameterButtonsElement _resolutionElement;
        [SerializeField] Toggle _fullscreenToggle;
        [SerializeField] ParameterButtonsElement _fontSizeElement;
        [SerializeField] ParameterButtonsElement _readingSpeedElement;

        [SerializeField] CanvasGroup _confirmationPopupCG;

        Animator _animator;
        Options _tempOptions;
        public Options GetTempOptions() { return _tempOptions; }
        bool _tempOptionsChanged;


        void Awake()
        {
            DisplayPopup(false);
        }
        /*********************************************************/

        void Start()
        {
            _animator = GetComponent<Animator>();

            _volumeValueText.text = _volumeSlider.value + " %";

            List<string> languageOptions = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(Language)))
                languageOptions.Add(value);
            _languageElement.Initialize(languageOptions);
            _languageElement.OnValueChanged += SetLanguage;

            List<string> resolutionOptions = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(Resolution)))
                resolutionOptions.Add(value);
            _resolutionElement.Initialize(resolutionOptions);
            _resolutionElement.OnValueChanged += SetResolution;

            List<string> fontSizeOptions = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(FontSize)))
                fontSizeOptions.Add(value);
            _fontSizeElement.Initialize(fontSizeOptions);
            _fontSizeElement.OnValueChanged += SetFontSize;

            List<string> readingSpeedOptions = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(ReadingSpeed)))
                readingSpeedOptions.Add(value);
            _readingSpeedElement.Initialize(readingSpeedOptions);
            _readingSpeedElement.OnValueChanged += SetReadingSpeed;
        }
        /*********************************************************/

        void DisplayPopup(bool a_show)
        {
            _confirmationPopupCG.alpha = a_show ? 1 : 0;
            _confirmationPopupCG.interactable = a_show;
            _confirmationPopupCG.blocksRaycasts = a_show;
        }
        /*********************************************************/

        void Initialize()
        {
            _tempOptions = new Options();
            _volumeSlider.value = _tempOptions.volume;
            _languageElement.SetValue(_tempOptions.language.ToString());
            _resolutionElement.SetValue(_tempOptions.resolution.ToString());
            _fullscreenToggle.isOn = _tempOptions.isFullscreen;
            _fontSizeElement.SetValue(_tempOptions.fontSize.ToString());
            _readingSpeedElement.SetValue(_tempOptions.readingSpeed.ToString());
            _tempOptionsChanged = false;
        }
        /*********************************************************/

        void SetLanguage(int a_valueIndex)
        {
            _tempOptions.language = (Language)a_valueIndex;
            _tempOptionsChanged = true;
        }
        /*********************************************************/

        void SetResolution(int a_valueIndex)
        {
            _tempOptions.resolution = (Resolution)a_valueIndex;
            _tempOptionsChanged = true;
        }
        /*********************************************************/

        void SetFontSize(int a_valueIndex)
        {
            _tempOptions.fontSize = (FontSize)a_valueIndex;
            _tempOptionsChanged = true;
        }
        /*********************************************************/

        void SetReadingSpeed(int a_valueIndex)
        {
            _tempOptions.readingSpeed = (ReadingSpeed)a_valueIndex;
            _tempOptionsChanged = true;
        }
        /*********************************************************/

        public void Btn_DisplayPanel()
        {
            _animator.SetBool("Show", true);
            Initialize();
        }
        /*********************************************************/

        public void Btn_QuitPanel()
        {
            if (_tempOptionsChanged)
                DisplayPopup(true);
            else
                Btn_HidePanel();
        }
        /*********************************************************/

        public void Btn_HidePopup()
        {
            DisplayPopup(false);
        }
        /*********************************************************/

        public void Btn_HidePanel()
        {
            _animator.SetBool("Show", false);
            DisplayPopup(false);
        }
        /*********************************************************/

        public void Btn_SaveOptions()
        {
            SaveLoad.SaveOptions(_tempOptions);
        }
        /*********************************************************/

        public void Btn_DisplayVolumeValue(float a_value)
        {
            _volumeValueText.text = _volumeSlider.value + " %";
            _tempOptions.volume = a_value;
            _tempOptionsChanged = true;
        }
        /*********************************************************/

        public void Btn_SetFullScreen(bool a_value)
        {
            _tempOptions.isFullscreen = a_value;
            _tempOptionsChanged = true;
        }
        /*********************************************************/
    }
}
