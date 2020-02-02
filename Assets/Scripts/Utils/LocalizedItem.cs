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


    ///////////////////////////////////////////////////////////////
    /// GENERAL FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void Awake()
        {
            _text = GetComponent<Text>();
        }
        /*********************************************************/

        void Start()
        {
            _text.text = Utils.Localization.GetLocalized(_localizedId);

            if (_hasToBeUpdated)
                ParametersPanel.Instance.OnOptionsChanged += OnOptionsChangedCallback;
        }
        /*********************************************************/
        
    ///////////////////////////////////////////////////////////////
    /// PRIVATE FUNCTIONS /////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        void OnOptionsChangedCallback()
        {
            _text.text = Utils.Localization.GetLocalized(_localizedId);
        }
        /*********************************************************/

    ///////////////////////////////////////////////////////////////
    /// PUBLIC FUNCTIONS //////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
        public void SetIdLocalized(string a_idLocalized)
        {
            _localizedId = a_idLocalized;
            //Debug.Log(_localizedId);
            _text.text = Utils.Localization.GetLocalized(_localizedId);
        }
        /*********************************************************/
    }
}
