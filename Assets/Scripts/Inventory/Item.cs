using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable/Item", order = 0)]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
}
