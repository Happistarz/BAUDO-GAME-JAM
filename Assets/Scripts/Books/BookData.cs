using UnityEngine;

[CreateAssetMenu(fileName = "BookData", menuName = "Scriptable/BookData")]
public class BookData : ScriptableObject
{
    public string title;
    public string content;
    public Sprite cover;
}
