using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;



public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;

    private static DialogueManager instance;

    private static bool dialogueIsPlaying = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one dialogue manager script in this scene");
        }
        instance = this; 
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
    public void EnterDialogue(TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else { }
    }
}
