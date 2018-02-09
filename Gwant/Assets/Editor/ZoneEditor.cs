using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Zone))]
public class ZoneEditor : Editor {

    public override void OnInspectorGUI()
    {
        Zone zone = (Zone)target;
        base.OnInspectorGUI();

        //EditorGUILayout.LabelField("Type", zone.Type.ToString().ToUpper());
        //zone.Type = (Zone.Types)EditorGUILayout.EnumPopup("Type", zone.Type);
        EditorGUILayout.EnumPopup("Type", zone.Type);
        //zone.VisibleTo = (Zone.IsVisibleTo)EditorGUILayout.EnumPopup("Visible to", zone.VisibleTo);
        EditorGUILayout.EnumPopup("Visible to", zone.VisibleTo);
        EditorGUILayout.Toggle("Collapsed", zone.IsCollapsed);
    }

    public static void DrawCardFoldout(List<Card> Cards, bool[] ActiveCards, bool ViewCards)
    {

    }

}
