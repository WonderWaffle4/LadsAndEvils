using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainOptions : MonoBehaviour
{
    public GameObject unActivePanel;
    public Button unActiveButton;
    public GameObject activePanel;
    public Button activeButton;

    private ColorBlock activeColor;
    private ColorBlock unActiveColor;

    public void Click()
    {
        activeColor = activeButton.colors;
        unActiveColor = unActiveButton.colors;

        activeButton.colors = unActiveColor;
        unActiveButton.colors = activeColor;

        unActivePanel.SetActive(true);
        activePanel.SetActive(false);
    }
}
