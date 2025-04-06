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
    public Image portraitImage;
    public GameObject dialoguePanel;
    public GameObject nextButton;
    public GameObject returnButton;


    private int currentLine = 0;

    void Start()
    {
        dialoguePanel.SetActive(true);
        returnButton.SetActive(false);
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
            dialoguePanel.SetActive(false);
            returnButton.SetActive(true);
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
