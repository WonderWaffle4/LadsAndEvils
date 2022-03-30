using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    public GameObject panel;
    public GameObject MainButton;

    public void Close()
    {
        panel.SetActive(false);
        transform.gameObject.SetActive(false);
        MainButton.SetActive(true);
    }
}
