using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Takeable))]
public class TakeableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Takeable takeable = (Takeable)target;

        // Dessiner les champs par défaut
        takeable.itemToTake = (Item)EditorGUILayout.ObjectField("Item To Take", takeable.itemToTake, typeof(Item), false);
        takeable.afterTake = (EffectAfterTake)EditorGUILayout.EnumPopup("After Take", takeable.afterTake);

        // Afficher itemNumber seulement si afterTake est Limited
        if (takeable.afterTake == EffectAfterTake.Limited)
        {
            takeable.itemNumber = EditorGUILayout.IntField("Item Number", takeable.itemNumber);
        }

        // Appliquer les modifications
        if (GUI.changed)
        {
            EditorUtility.SetDirty(takeable);
        }
    }
}