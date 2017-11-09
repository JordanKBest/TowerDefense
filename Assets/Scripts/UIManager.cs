using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject towerPanel;
    bool showPanel = false;

    private static UIManager Current;
    private void Awake()
    {
        if(Current == null)
        {
            Current = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        towerPanel.SetActive(showPanel);
    }

    public void TogglePanel()
    {
        showPanel = !showPanel;
        towerPanel.SetActive(showPanel);
    }


    public void OnClick()
    {
        GameController.Current.StartLevel();
    }
}
