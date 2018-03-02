using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneTrigger))]
public class ZoneTriggerEditor : Editor {

    SerializedProperty image;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        ZoneTrigger trigger = (ZoneTrigger)target;

        trigger.DefaultColour = EditorGUILayout.ColorField("Default Colour", trigger.DefaultColour);
        trigger.HighlightColour = EditorGUILayout.ColorField("Highlight Colour", trigger.HighlightColour);
        EditorGUILayout.Space();
        string imageInfo = (trigger.ZoneImage == null) ? "(null) - will set at runtime" : trigger.ZoneImage.gameObject.ToString();
        EditorGUILayout.LabelField("Image", imageInfo);
        EditorGUILayout.Toggle("Highlighted", trigger.Highlighted);

        //SerializedProperty p = serializedObject.FindProperty("testString");
        //EditorGUILayout.PropertyField(p);
        SerializedProperty p2 = serializedObject.FindProperty("privateString");
        EditorGUILayout.PropertyField(p2);
    }

}
