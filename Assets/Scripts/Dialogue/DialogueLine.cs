using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    [TextArea(2, 5)]
    public string text;
}
