#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(Malediction))]
public class MaledictionEditor : Editor
{
    private SerializedProperty maledictionNameProp;
    private SerializedProperty prerequirementsProp;
    private SerializedProperty effectsProp;
    
    private void OnEnable()
    {
        maledictionNameProp = serializedObject.FindProperty("maledictionName");
        prerequirementsProp = serializedObject.FindProperty("prerequirements");
        effectsProp = serializedObject.FindProperty("maledictionEffects");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        // Draw malediction name
        EditorGUILayout.PropertyField(maledictionNameProp);
        
        // Draw Prerequirements Section
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Prerequirements", EditorStyles.boldLabel);
        
        if (prerequirementsProp.arraySize > 0)
        {
            for (int i = 0; i < prerequirementsProp.arraySize; i++)
            {
                DrawArrayElement(prerequirementsProp, i, () => RemovePrerequirement(i));
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No prerequirements added yet.", MessageType.Info);
        }
        
        if (GUILayout.Button("Add Prerequirement", GUILayout.Height(30)))
        {
            ShowAddPrerequirementMenu();
        }
        
        // Draw Effects Section
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);
        
        if (effectsProp.arraySize > 0)
        {
            for (int i = 0; i < effectsProp.arraySize; i++)
            {
                DrawArrayElement(effectsProp, i, () => RemoveEffect(i));
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No effects added yet.", MessageType.Info);
        }
        
        if (GUILayout.Button("Add Effect", GUILayout.Height(30)))
        {
            ShowAddEffectMenu();
        }
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private void DrawArrayElement(SerializedProperty arrayProp, int index, Action onRemove)
    {
        SerializedProperty element = arrayProp.GetArrayElementAtIndex(index);
        
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        
        // Get the type name and display it
        string typeName = element.managedReferenceFullTypename;
        string displayName = "Empty";
        
        if (!string.IsNullOrEmpty(typeName))
        {
            // Extract just the class name from the full typename
            string[] splitName = typeName.Split(' ');
            if (splitName.Length > 1)
            {
                displayName = splitName[1].Split('.').Last();
            }
        }
        
        element.isExpanded = EditorGUILayout.Foldout(element.isExpanded, $"{index}: {displayName}", true);
        
        // Remove button
        if (GUILayout.Button("X", GUILayout.Width(25)))
        {
            onRemove?.Invoke();
        }
        
        EditorGUILayout.EndHorizontal();
        
        // Draw properties if expanded
        if (element.isExpanded)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(element, GUIContent.none, true);
            EditorGUI.indentLevel--;
        }
        
        EditorGUILayout.EndVertical();
        GUILayout.Space(5);
    }
    
    private void ShowAddPrerequirementMenu()
    {
        GenericMenu menu = new GenericMenu();
        
        // Find all types that inherit from Prerequirement
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(Prerequirement)) && !t.IsAbstract);
        
        foreach (var type in types)
        {
            menu.AddItem(new GUIContent(type.Name), false, () => AddPrerequirement(type));
        }
        
        menu.ShowAsContext();
    }
    
    private void AddPrerequirement(Type type)
    {
        Malediction malediction = (Malediction)target;
        
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
        serializedObject.Update();
    }
    
    private void RemovePrerequirement(int index)
    {
        Malediction malediction = (Malediction)target;
        
        Undo.RecordObject(malediction, "Remove Prerequirement");
        
        var list = malediction.prerequirements.ToList();
        list.RemoveAt(index);
        malediction.prerequirements = list.ToArray();
        
        EditorUtility.SetDirty(malediction);
        serializedObject.Update();
    }
    
    private void ShowAddEffectMenu()
    {
        GenericMenu menu = new GenericMenu();
        
        // Find all types that inherit from MaledictionEffect
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(MaledictionEffect)) && !t.IsAbstract);
        
        foreach (var type in types)
        {
            menu.AddItem(new GUIContent(type.Name), false, () => AddEffect(type));
        }
        
        menu.ShowAsContext();
    }
    
    private void AddEffect(Type type)
    {
        Malediction malediction = (Malediction)target;
        
        Undo.RecordObject(malediction, "Add Effect");
        
        var newEffect = (MaledictionEffect)Activator.CreateInstance(type);
        
        if (malediction.maledictionEffects == null)
        {
            malediction.maledictionEffects = new MaledictionEffect[] { newEffect };
        }
        else
        {
            var list = malediction.maledictionEffects.ToList();
            list.Add(newEffect);
            malediction.maledictionEffects = list.ToArray();
        }
        
        EditorUtility.SetDirty(malediction);
        serializedObject.Update();
    }
    
    private void RemoveEffect(int index)
    {
        Malediction malediction = (Malediction)target;
        
        Undo.RecordObject(malediction, "Remove Effect");
        
        var list = malediction.maledictionEffects.ToList();
        list.RemoveAt(index);
        malediction.maledictionEffects = list.ToArray();
        
        EditorUtility.SetDirty(malediction);
        serializedObject.Update();
    }
}
#endif