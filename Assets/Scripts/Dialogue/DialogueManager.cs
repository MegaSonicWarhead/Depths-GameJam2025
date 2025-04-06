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
        startGameButton.gameObject.SetActive(false);  
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
            //dialoguePanel.SetActive(false);
            startGameButton.gameObject.SetActive(true);
        }
    }

    public void AdvanceDialogue()
    {
        currentLine++;
        ShowLine();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); 
    }
}

