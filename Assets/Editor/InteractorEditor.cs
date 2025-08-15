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
        
        EditorGUILayout.LabelField("Certified Whore-er Game Component", EditorStyles.centeredGreyMiniLabel);
        
        GUI.contentColor = new Color(0.8f, 0.8f, 1f);
        GUI.backgroundColor = new Color(1, 1, 1);
        GUIStyle myBoxStyle = new GUIStyle("Window");
        myBoxStyle.padding = new RectOffset(10, 10, 10, 10);
        myBoxStyle.margin = new RectOffset(0, 20, 10, 0);
        EditorGUILayout.BeginVertical(myBoxStyle); // Hover effects Box ================================================
        
        // Possible Hover Effects
        EditorGUILayout.LabelField("On Hover Effects", EditorStyles.boldLabel);
        
        EditorGUILayout.LabelField("Choose hover effects for this object:", EditorStyles.label);
        SerializedProperty hoverEffectsProp = serializedObject.FindProperty("hoverEffects");
        EditorGUILayout.PropertyField(hoverEffectsProp, new GUIContent("Hover Effects"), true);
        
        EditorGUILayout.Space(5);
        
        
        EditorGUILayout.EndVertical(); // Hover effects Box ============================================================

        EditorGUILayout.BeginVertical(myBoxStyle); // Events Box =======================================================
        
        //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); DELETE
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
        //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); DELETE
        
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
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace(); // Push to center
            GUIStyle buttonStyle = new GUIStyle("Button");
            hasCustomKey.boolValue = GUILayout.Toggle(hasCustomKey.boolValue, "Custom Key Interaction", "Button");
            GUILayout.FlexibleSpace(); // Push to center
            EditorGUILayout.EndHorizontal();
            
            EditorGUI.indentLevel++;
            EditorGUILayout.Space(10);

            if (hasCustomKey.boolValue)
            {
                SerializedProperty customKey = serializedObject.FindProperty("customInteractionKey");
                SerializedProperty customKeyEvent = serializedObject.FindProperty("OnCustomKeyPressed");

                EditorGUILayout.PropertyField(customKey, new GUIContent("Select Custom Key"));
                EditorGUILayout.PropertyField(customKeyEvent);
                
            }
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical(); // Events Box ===================================================================
        
        serializedObject.ApplyModifiedProperties();
    }
}
