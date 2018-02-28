using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneTrigger))]
public class ZoneTriggerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SerializedProperty image = serializedObject.FindProperty("myImage");
        EditorGUILayout.PropertyField(image);

    }

}
