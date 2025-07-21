using UnityEditor;
using UnityEngine;

/// <summary>
/// This is an editor extension class for the Interactor component.
/// It's only purpose is to style to inspector view of the interactor component for easy to read access.
/// </summary>
[CustomEditor(typeof(Interactor))]
public class InteractorEditor : Editor
{
    private bool showMouseEvents = false;
    private bool showButtonEvents = false;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        // Change color of inspector \/
        Color originalColor = GUI.backgroundColor;
        GUI.backgroundColor = new Color(0.757f, 0.878f, 0.757f, 1f);
        EditorGUILayout.BeginVertical("box");
        // Change color of inspector /\
        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Interactor Component Base Events", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);
        
        // A fold out view of all the mouse events.
        EditorGUI.indentLevel++;
        showMouseEvents = EditorGUILayout.Foldout(showMouseEvents, "Mouse Click Events");
        if (showMouseEvents)
        {
            SerializedProperty onEnter = serializedObject.FindProperty("OnMouseEnter");
            SerializedProperty onExit = serializedObject.FindProperty("OnMouseExit");
            SerializedProperty onClick = serializedObject.FindProperty("OnMouseClick");

            EditorGUILayout.PropertyField(onEnter);
            EditorGUILayout.PropertyField(onExit);
            EditorGUILayout.PropertyField(onClick);
        }
        EditorGUI.indentLevel--;
        
        // Horizontal divider.
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        // A fold out view of all the button pressed events.
        EditorGUI.indentLevel++;
        showButtonEvents = EditorGUILayout.Foldout(showButtonEvents, "Button Pressed Events");
        if (showButtonEvents)
        {
            // Interaction Key Pressed Event;
            EditorGUILayout.Space(10);
            SerializedProperty onInteract = serializedObject.FindProperty("OnInteractionKeyPressed");
            EditorGUILayout.PropertyField(onInteract);
            
            // Include possible field for custom key interaction event.
            EditorGUI.indentLevel--;
            SerializedProperty hasCustomKey = serializedObject.FindProperty("hasCustomKeyInteraction");
            EditorGUILayout.PropertyField(hasCustomKey);
            EditorGUI.indentLevel++;
            EditorGUILayout.Space(10);

            if (hasCustomKey.boolValue)
            {
                SerializedProperty customKey = serializedObject.FindProperty("customInteractionKey");
                SerializedProperty customKeyEvent = serializedObject.FindProperty("OnCustomKeyPressed");

                EditorGUILayout.PropertyField(customKey);
                EditorGUILayout.PropertyField(customKeyEvent);
                
            }
        }
        EditorGUI.indentLevel--;
        
        EditorGUILayout.EndVertical();
        
        serializedObject.ApplyModifiedProperties();
    }
}
