using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace TheWill
{
    public class ParameterButtonsElement : MonoBehaviour
    {
        [SerializeField] Text _valueText;
        [SerializeField] Button _previousButton;
        [SerializeField] Button _nextButton;
        [SerializeField] List<string> _options = new List<string>();
        int _currentIndex;
        public int GetCurrentIndex() { return _currentIndex; }
        ParametersPanel _parametersPanel;

        public Action<int> OnValueChanged;

        
    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            if (_valueText == null)
                _valueText = GetComponentInChildren<Text>();
            if (_previousButton == null)
                _previousButton = GetComponentsInChildren<Button>()[0];
            if (_nextButton == null)
                _nextButton = GetComponentsInChildren<Button>()[1];
            if (_options.Count == 0)
                _options.Add(_valueText.text);
        }
        /*********************************************************/

        void Start()
        {
            _currentIndex = 0;
        }
        /*********************************************************/

        void UpdateValue()
        {
            _currentIndex = _currentIndex % (_options.Count);
            _valueText.text = _options[_currentIndex];
            if (OnValueChanged != null)
                OnValueChanged(_currentIndex);
        }
        /*********************************************************/

        ///////////////////////////////////////////////////////////////
        /// PRIVATE FUNCTIONS /////////////////////////////////////////
        ///////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////
        /// PUBLIC FUNCTIONS /////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        public void Initialize(List<string> a_options)
        {
            _options.Clear();
            foreach (string option in a_options)
                _options.Add(option/*Localization.GetLocalized(option)*/);
            _currentIndex = 0;
            _valueText.text = _options[0];
        }
        /*********************************************************/

        public void SetValue(string a_value)
        {
            if (_options.Contains(a_value) == false)
            {
                Debug.LogError("There is no value (" + a_value + ") in " + gameObject.name);
                return;
            }

            for (int i = 0; i < _options.Count; i++)
            {
                if (_options[i] == a_value)
                {
                    _currentIndex = i;
                    _valueText.text = _options[i];
                }
            }
        }
        /*********************************************************/

        public void Btn_NextOption()
        {
            _currentIndex++;
            UpdateValue();
        }
        /*********************************************************/

        public void Btn_PrevOption()
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _options.Count - 1;
            UpdateValue();
        }
        /*********************************************************/
    }
}
