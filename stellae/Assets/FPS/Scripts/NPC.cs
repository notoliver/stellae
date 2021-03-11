using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC file", menuName = "NPC Files Archive")]
public class NPC : ScriptableObject
{
    public string name;
    // Gives us more room to work with inside the editor for dialouge
    [TextArea(3, 15)]
    public string[] dialogue;
    // Gives us more room to work with inside the editor for PLAYER dialogue responses
    [TextArea(3, 15)]
    public string[] playerDialogue;
}