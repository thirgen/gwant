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
        EditorGUILayout.LabelField("Image", trigger.Image.gameObject.ToString());
        EditorGUILayout.Toggle("Highlighted", trigger.Highlighted);
    }

}
