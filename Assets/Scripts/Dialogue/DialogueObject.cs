using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Replique
{
    public string text;
    public string speaker;
    public Sprite portrait;
    // unity callback for when the dialogue is finished
    [CanBeNull] public UnityEngine.Events.UnityEvent onDialogueEnd;
}

[CreateAssetMenu(fileName = "DialogueObject", menuName = "Scriptable/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    public string Name;
    public Replique[] Replies;
    public DialogueCondition Condition;
}

public enum DialogueCondition
{
    None,
    QuestCompleted,
}