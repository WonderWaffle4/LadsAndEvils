using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    public Button start_game;
    
    public void LoadScene()
    {
        SceneManager.LoadScene("test");
    }
}
