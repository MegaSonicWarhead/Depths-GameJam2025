using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class WinDialogueManager : MonoBehaviour
{
    public DialogueData dialogueData;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    //public Image portraitImage;
    public GameObject dialoguePanel;
    public GameObject nextButton;
    public GameObject returnButton;

    private int currentLine = 0;

    void Start()
    {
        dialoguePanel.SetActive(true);
        nextButton.SetActive(true);  // Show the Next button
        returnButton.SetActive(false); // Hide the Return button initially
        ShowLine();
    }

    public void ShowLine()
    {
        if (currentLine < dialogueData.lines.Length)
        {
            DialogueLine line = dialogueData.lines[currentLine];
            speakerText.text = line.speaker;
            dialogueText.text = line.text;
            //portraitImage.sprite = Resources.Load<Sprite>(line.portrait);
        }
        else
        {
            nextButton.SetActive(false);  // Hide the Next button
            returnButton.SetActive(true); // Show the Return button when dialogue ends
        }
    }

    public void AdvanceDialogue()
    {
        currentLine++;
        ShowLine();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Make sure the scene is added in Build Settings
    }
}
