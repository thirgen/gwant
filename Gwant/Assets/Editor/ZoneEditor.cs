using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Zone), true)]
public class ZoneEditor : Editor {

    bool cards = false;
    bool[] active;
    static AnimationCurve curve;

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
        if (cards)
            DrawCardFoldout(zone, ref active);
    }

    public static void DrawCardFoldout(Zone zone, ref bool[] ActiveCards)
    {
        if (zone.Cards != null)
        {
            if (ActiveCards == null || ActiveCards.Length != zone.Cards.Count)
            {
                ActiveCards = new bool[zone.Cards.Count];
                for (int i = 0; i < ActiveCards.Length; i++)
                    ActiveCards[i] = false;
            }
            for (int i = 0; i < zone.Cards.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                ActiveCards[i] = EditorGUILayout.Foldout(ActiveCards[i], zone.Cards[i].Card.Name);
                EditorGUILayout.EndHorizontal();

                if (ActiveCards[i])
                {
                    EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

                    GUILayout.Space(40); //Tab in 20 pixels //https://forum.unity.com/threads/indenting-guilayout-objects.113494/

                    EditorGUILayout.BeginVertical(); //Vertical contains ints
                    if (zone.Cards[i].Card.Special)
                        SpecialCardEditor.DrawInspector((SpecialCard)zone.Cards[i].Card);
                    else
                        UnitCardEditor.DrawInspector((UnitCard)zone.Cards[i].Card);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

}
