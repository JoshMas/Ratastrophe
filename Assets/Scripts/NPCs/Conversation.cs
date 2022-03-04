using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    public string speaker;
    [TextArea(5, 10)]
    public string text;
}

[CreateAssetMenu(menuName ="Dialogue/Basic")]
public class Conversation : ScriptableObject
{
    [SerializeField]
    private List<Dialogue> dialogue;

    public void BeginDialogue()
    {

    }
}
