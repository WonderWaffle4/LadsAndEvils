using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Option : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject MainButton;
    public GameObject OptionButton;

    public void Click()
    {
        OptionsPanel.SetActive(true);
        MainButton.SetActive(false);
        OptionButton.SetActive(true);
    }
}
