using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public DialogueSequence sequence;
    public TextMeshProUGUI dialogueText;
    private int index = 0;
    private bool inDialogue = false;

    void Start()
    {
        dialogueText.text = "";
    }

    void Update()
    {
        if (inDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialogue();
        }
    }

    public void StartDialogue(DialogueSequence newSequence)
    {
        sequence = newSequence;
        index = 0;
        inDialogue = true;
        dialogueText.text = sequence.lines[index].text;
    }

    private void AdvanceDialogue()
    {
        index++;
        if (index >= sequence.lines.Length)
        {
            dialogueText.text = "";
            inDialogue = false;
            return;
        }
        dialogueText.text = sequence.lines[index].text;
    }
}
