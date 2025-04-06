using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public DialogueData dialogueData;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public GameObject dialoguePanel;
    public GameObject startGameButton;

    private int currentLine = 0;


    void Start()
    {
        dialoguePanel.SetActive(true);
        startGameButton.gameObject.SetActive(false);  // Ensure Start Game button is hidden initially
        ShowLine();
    }

    public void ShowLine()
    {
        if (currentLine < dialogueData.lines.Length)
        {
            DialogueLine line = dialogueData.lines[currentLine];
            speakerText.text = line.speaker;
            dialogueText.text = line.text;
            portraitImage.sprite = Resources.Load<Sprite>(line.portrait);
        }
        else
        {
            dialoguePanel.SetActive(false); // Hide dialogue panel after last line
            startGameButton.gameObject.SetActive(true);  // Show the Start Game button
        }
    }

    public void AdvanceDialogue()
    {
        currentLine++;
        ShowLine();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Replace "Game" with the actual name of your game scene
    }
}

