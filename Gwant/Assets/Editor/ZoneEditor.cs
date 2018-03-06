using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Zone), true)]
public class ZoneEditor : Editor {

    bool cards = false;
    bool[] active;

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

        //EditorGUILayout.Toggle("Highlighted", zone.Highlighted);
        cards = EditorGUILayout.Foldout(cards, "Cards");
        if (zone.Cards != null && cards)
        {
            if (active == null || active.Length != zone.Cards.Count)
            {
                active = new bool[zone.Cards.Count];
                for (int i = 0; i < active.Length; i++)
                    active[i] = false;
            }
            for (int i = 0; i < zone.Cards.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                active[i] = EditorGUILayout.Foldout(active[i], zone.Cards[i].Card.Name);
                EditorGUILayout.EndHorizontal();

                if (active[i])
                {
                    EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

                    GUILayout.Space(40); //Tab in 20 pixels //https://forum.unity.com/threads/indenting-guilayout-objects.113494/

                    EditorGUILayout.BeginVertical(); //Vertical contains ints
                    UnitCardEditor.DrawInspector((UnitCard)zone.Cards[i].Card);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

    public static void DrawCardFoldout(List<Card> Cards, bool[] ActiveCards, bool ViewCards)
    {

    }

}
