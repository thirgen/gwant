using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Battlefield))]
public class BattlefieldEditor : Editor
{
    bool cards = false;
    bool[] active;

    public override void OnInspectorGUI()
    {

        Battlefield zone = (Battlefield)target;
        //base.OnInspectorGUI();
        EditorGUILayout.EnumPopup("Type", zone.Type);
        
        SerializedProperty hornZone = serializedObject.FindProperty("hornZone");
        EditorGUILayout.PropertyField(hornZone);

        EditorGUILayout.IntField("Morale", zone.Morale);

        cards = EditorGUILayout.Foldout(cards, "Cards");
        if (cards)
            ZoneEditor.DrawCardFoldout(zone, ref active);
    }

}
