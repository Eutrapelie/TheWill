using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheWill
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField]
        Button _buttonClose;

        [SerializeField]
        Button _buttonSave;

        [SerializeField]
        Text _actDayText;

        [SerializeField]
        List<HUDView> _hudView;

        GameObject activePanel;


        void Awake()
        {
            foreach (HUDView view in _hudView)
            {
                view.buttonMenu.onClick.AddListener(() => OpenPanel(view.panelMenu));
            }
            _buttonClose.onClick.AddListener(ClosePanel);
            _buttonClose.gameObject.SetActive(false);

            _buttonSave.onClick.AddListener(Btn_Save);
        }

        void OpenPanel(GameObject panel)
        {
            activePanel = panel;
            panel.SetActive(true);
            _buttonClose.gameObject.SetActive(true);
            ToggleHUDView(false);
        }

        void ToggleHUDView(bool show)
        {
            foreach (HUDView view in _hudView)
            {
                view.buttonMenu.gameObject.SetActive(show);
            }
        }

        void ClosePanel()
        {
            activePanel.SetActive(false);
            activePanel = null;
            _buttonClose.gameObject.SetActive(false);
            ToggleHUDView(true);
        }

        void Btn_Save()
        {
            SaveLoad.Save();
        }
    }
}
