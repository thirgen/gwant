using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneEventTrigger))]
public class ZoneEventTriggerEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        ZoneEventTrigger trigger = (ZoneEventTrigger)target;

        EditorGUILayout.LabelField("Static Members");
        trigger.DefaultColour = EditorGUILayout.ColorField("Default Colour", trigger.DefaultColour);
        trigger.HighlightColour = EditorGUILayout.ColorField("Highlight Colour", trigger.HighlightColour);

        
        //https://answers.unity.com/questions/216584/horizontal-line.html
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);


        EditorGUILayout.LabelField("Non-Static Members");
        SerializedProperty image = serializedObject.FindProperty("image");
        if (image != null)
            EditorGUILayout.PropertyField(image);

    }

}
