using UnityEngine;
using UnityEngine.UI;

namespace TheWill
{
    [RequireComponent(typeof(Text))]
    public class LocalizedItem : MonoBehaviour
    {
        [SerializeField] string _localizedId;
        [SerializeField] bool _hasToBeUpdated;
        Text _text;
        
        void Start()
        {
            _text = GetComponent<Text>();
            //Debug.Log(_localizedId);
            _text.text = Utils.Localization.GetLocalized(_localizedId);

            if (_hasToBeUpdated)
                ParametersPanel.Instance.OnOptionsChanged += OnOptionsChangedCallback;
        }

        void OnOptionsChangedCallback()
        {
            _text.text = Utils.Localization.GetLocalized(_localizedId);
        }
    }
}
