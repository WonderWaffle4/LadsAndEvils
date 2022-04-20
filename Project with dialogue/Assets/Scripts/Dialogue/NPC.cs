using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class NPC : MonoBehaviour
{
    [Header("StartDialogue")]
    [SerializeField] private GameObject startDialogue;
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJson;
    private bool playerInRange;
    private bool dialogueIsActive = false;

    private Story currentstory;
    private void Show()
    {
        playerInRange = false;
        startDialogue.SetActive(false);
        dialoguePanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerInRange =true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerInRange = false;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.M) && playerInRange && !dialogueIsActive)
        {
            EnterDialogueMode(InkJson);
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerInRange && dialogueIsActive)
        {
            coninueStory();
        }
        if (dialogueIsActive && playerInRange)
        {
            startDialogue.SetActive(false);
            dialoguePanel.SetActive(true);
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue.SetActive(playerInRange);
            dialoguePanel.SetActive(false);
            dialogueIsActive = false;
        }
    }
    public void EnterDialogueMode(TextAsset InkJson)
    {
        dialogueIsActive = true;
        currentstory = new Story(InkJson.text);
        coninueStory();
    }
    public void ExitDialogueMode()
    {
        dialogueIsActive = false;
        dialogueText.text = "";
    }

    private void coninueStory()
    {
        if (currentstory.canContinue)
        {
            dialogueText.text = currentstory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }
}
