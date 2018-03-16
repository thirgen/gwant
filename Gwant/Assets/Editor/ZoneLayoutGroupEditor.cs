using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZoneLayoutGroup))]
public class ZoneLayoutGroupEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        ZoneLayoutGroup group = (ZoneLayoutGroup)target;

        SerializedProperty zone = serializedObject.FindProperty("zone");
        EditorGUILayout.PropertyField(zone);

        group.spacing = EditorGUILayout.FloatField("Spacing", group.spacing);
        SerializedProperty cardPos = serializedObject.FindProperty("newCardPosition");
        EditorGUILayout.PropertyField(cardPos);
    }


}
