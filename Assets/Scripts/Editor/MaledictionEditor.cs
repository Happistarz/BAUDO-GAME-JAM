#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(Malediction))]
public class MaledictionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Malediction malediction = (Malediction)target;
        
        GUILayout.Space(10);
        
        if (GUILayout.Button("Add Prerequirement"))
        {
            ShowAddPrerequirementMenu(malediction);
        }
    }
    
    private void ShowAddPrerequirementMenu(Malediction malediction)
    {
        GenericMenu menu = new GenericMenu();
        
        // Find all types that inherit from Prerequirement
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(Prerequirement)) && !t.IsAbstract);
        
        foreach (var type in types)
        {
            menu.AddItem(new GUIContent(type.Name), false, () => AddPrerequirement(malediction, type));
        }
        
        menu.ShowAsContext();
    }
    
    private void AddPrerequirement(Malediction malediction, Type type)
    {
        Undo.RecordObject(malediction, "Add Prerequirement");
        
        var newPrerequirement = (Prerequirement)Activator.CreateInstance(type);
        
        if (malediction.prerequirements == null)
        {
            malediction.prerequirements = new Prerequirement[] { newPrerequirement };
        }
        else
        {
            var list = malediction.prerequirements.ToList();
            list.Add(newPrerequirement);
            malediction.prerequirements = list.ToArray();
        }
        
        EditorUtility.SetDirty(malediction);
    }
}
#endif