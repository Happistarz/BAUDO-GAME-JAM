using UnityEngine;

public class DialogueInteractable : MonoBehaviour, IInteractable
{
    public DialogueObject dialogue;
    public DialogueSystem dialogueSystem;
    
    public string InteractionText => "Talk";

    public void Interact()
    {
        dialogueSystem.PlayDialogue(dialogue);
    }

    public bool IsInteractalbe()
    {
        return true;
    }
}