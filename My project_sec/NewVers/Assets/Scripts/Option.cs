using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainButton;
    public GameObject optionButton;

    [Header("Active")]
    public Button firstButton;
    public GameObject firstPanel;

    [Header("UnActive")]
    public Button secButton;
    public GameObject secPanel;

    private ColorBlock activeColor;
    private ColorBlock unActiveColor;

    public void Click()
    {
        if(secPanel.activeSelf == true)
        {
            activeColor = secButton.colors;
            unActiveColor = firstButton.colors;

            firstButton.colors = activeColor;
            secButton.colors = unActiveColor;

            firstPanel.SetActive(true);
            secPanel.SetActive(false);
        }

        optionsPanel.SetActive(true);
        mainButton.SetActive(false);
        optionButton.SetActive(true);
    }
}
