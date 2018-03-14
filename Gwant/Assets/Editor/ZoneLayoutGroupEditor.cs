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
        group.spacing = EditorGUILayout.FloatField("Spacing", group.spacing);
    }


}
