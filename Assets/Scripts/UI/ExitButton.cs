using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Function to be called when the Exit button is pressed
    public void OnExitButtonPressed()
    {
#if UNITY_EDITOR
        // In the editor, we log a message since Application.Quit doesn't actually close the editor
        Debug.Log("Exit Game (Editor Mode)");
#else
        // This will quit the application in a built game
        Application.Quit();
        Debug.Log("Quit Game");
#endif
    }
}
