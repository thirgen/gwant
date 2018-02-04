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
        zone.Type = (Zone.Types)EditorGUILayout.EnumPopup("Type", zone.Type);


        GUILayout.Label("Visible To");
        EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

        GUILayout.Space(20); //Tab in 20 pixels

        EditorGUILayout.BeginVertical(); //Vertical contains ints
        bool p1 = (zone.VisibleTo == Zone.IsVisibleTo.Player1 || zone.VisibleTo == Zone.IsVisibleTo.Both) ? true : false;
        bool p2 = (zone.VisibleTo == Zone.IsVisibleTo.Player2 || zone.VisibleTo == Zone.IsVisibleTo.Both) ? true : false;
        EditorGUILayout.Toggle("Player 1", p1);
        EditorGUILayout.Toggle("Player 2", p2);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

    }

}
