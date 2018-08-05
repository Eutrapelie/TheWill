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
        [SerializeField] ParameterButtonsElement _fontSizeElement;
        [SerializeField] ParameterButtonsElement _readingSpeedElement;

        [SerializeField] CanvasGroup _confirmationPopupCG;

        Animator _animator;


        void Awake()
        {
            DisplayPopup(false);
        }
        /*********************************************************/

        void Start()
        {
            _animator = GetComponent<Animator>();

            _volumeValueText.text = _volumeSlider.value + " %";

            List<string> languageOption = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(Language)))
                languageOption.Add(value);
            _languageElement.Initialize(languageOption);

            List<string> resolutionOption = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(Resolution)))
                resolutionOption.Add(value);
            _resolutionElement.Initialize(resolutionOption);

            List<string> fontSizeOption = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(FontSize)))
                fontSizeOption.Add(value);
            _fontSizeElement.Initialize(fontSizeOption);

            List<string> readingSpeedOption = new List<string>();
            foreach (string value in System.Enum.GetNames(typeof(ReadingSpeed)))
                readingSpeedOption.Add(value);
            _readingSpeedElement.Initialize(readingSpeedOption);
        }
        /*********************************************************/

        void Update()
        {

        }
        /*********************************************************/

        void DisplayPopup(bool a_show)
        {
            _confirmationPopupCG.alpha = a_show ? 1 : 0;
            _confirmationPopupCG.interactable = a_show;
            _confirmationPopupCG.blocksRaycasts = a_show;
        }
        /*********************************************************/

        public void Btn_DisplayPanel()
        {
            _animator.SetBool("Show", true);
        }
        /*********************************************************/

        public void Btn_QuitPanel()
        {
            DisplayPopup(true);
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

        public void Btn_DisplayVolumeValue(float a_value)
        {
            _volumeValueText.text = _volumeSlider.value + " %";
        }
    }
}
