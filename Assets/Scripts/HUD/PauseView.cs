using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField]
    private Button _buttonClose;

    [SerializeField]
    private Button _buttonSave;

    [SerializeField]
    private Text _actDayText;

    [SerializeField]
    private List<HUDView> _hudView;

    private GameObject activePanel;

    void Awake()
    {
        foreach (HUDView view in _hudView)
        {
            view.buttonMenu.onClick.AddListener(()=>OpenPanel(view.panelMenu));
        }
        _buttonClose.onClick.AddListener(ClosePanel);
        _buttonClose.gameObject.SetActive(false);

        _buttonSave.onClick.AddListener(Save);
    }

    private void OpenPanel(GameObject panel)
    {
        activePanel = panel;
        panel.SetActive(true);
        _buttonClose.gameObject.SetActive(true);
        ToggleHUDView(false);
    }

    private void ToggleHUDView(bool show)
    {
        foreach (HUDView view in _hudView)
        {
            view.buttonMenu.gameObject.SetActive(show);
        }
    }

    private void ClosePanel()
    {
        activePanel.SetActive(false);
        activePanel = null;
        _buttonClose.gameObject.SetActive(false);
        ToggleHUDView(true);
    }

    private void Save()
    {
        SaveLoad.Save();
    }

}
