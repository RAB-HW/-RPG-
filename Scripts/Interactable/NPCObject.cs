using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCObject : InteractableObject
{
    public string npcName;
    public string[] contentList;

    public DialogueUI dialogueUI;
    protected override void Interact()
    {
        print("Ineracting with NPC!!!");
        DialogueUI.Instance.Show(npcName, contentList);
    }
}
