using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    private static readonly int End = Animator.StringToHash("END");
    private static readonly int Start1 = Animator.StringToHash("START");
    
    [Header("Components")] 
    [SerializeField] private Animator dialogueAnimator;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI skipText;
    
    [Header("Settings")]
    [SerializeField] private float textTypingSpeed = 0.5f;
    [SerializeField] private float textTypingSoundDelay = 0.5f;
    [SerializeField] private InputActionReference skipDialogueAction;
    
    public bool isTyping;
    public bool isEnabled = true;
    
    [SerializeField] private DialogueObject currentDialogue;

    private int _index;
    
    private void Start()
    {
        skipDialogueAction.action.performed += _ => SkipDialogue();
        
        skipDialogueAction.action.Enable();
    }

    public void PlayDialogue(DialogueObject dialogue)
    {
        if (GameManager.Instance.OnUI) return;
        GameManager.Instance.OnUI = true;
        
        isEnabled = true;
        dialogueAnimator.SetTrigger(Start1);
        currentDialogue = dialogue;
        StartCoroutine(StartDialogue());
    }

    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.5f); // Delay before starting the dialogue
        _index = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private void NextLine()
    {
        _index++;
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
        currentDialogue.Replies[_index].onDialogueEnd?.Invoke();
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        StartCoroutine(TypingSoundCoroutine());
        var currentReply = currentDialogue.Replies[_index];
        
        speakerText.text = currentReply.speaker;
        
        foreach (var c in currentReply.text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textTypingSpeed);
        }
        isTyping = false;
    }

    public void SkipDialogue()
    {
        if (!isEnabled || currentDialogue == null) return;
        
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentDialogue.Replies[_index].text;
            isTyping = false;
            return;
        }

        skipText.text = _index < currentDialogue.Replies.Length - 2 
            ? "SKIP" 
            : "END";
        
        if (_index == currentDialogue.Replies.Length - 1)
        {
            EndDialogue();
            return;
        }
        
        NextLine();
    }

    private void EndDialogue()
    {
        dialogueAnimator.SetTrigger(End);
        isTyping = false;
        isEnabled = false;
        currentDialogue = null;
        
        GameManager.Instance.OnUI = false;
    }
    
    private IEnumerator TypingSoundCoroutine()
    {
        while (isTyping)
        {
            yield return new WaitForSeconds(textTypingSoundDelay);
        }
    }

    public void ONDIALOGUEEND()
    {
        // TEMP METHOD TO TEST THE END OF DIALOGUE
        Debug.Log("Dialogue ended.");
    }
}
