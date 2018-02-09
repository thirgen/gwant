using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Faction))]
public class FactionEditor : Editor
{
    static bool ViewCards = false;
    bool[] ActiveCards;

    public override void OnInspectorGUI()
    {
        Faction faction = (Faction)target;
        //base.OnInspectorGUI();
        
        EditorGUILayout.LabelField("Name", faction.Name);
        EditorGUILayout.Toggle("Exclusive", faction.Exclusive);

        ViewCards = EditorGUILayout.Foldout(ViewCards, "Cards");
        if (ViewCards)
        {
            if (ActiveCards == null)
            {
                ActiveCards = new bool[faction.Cards.Count];
                for (int i = 0; i < ActiveCards.Length; i++)
                {
                    ActiveCards[i] = false;
                }
            }
            for (int i = 0; i < faction.Cards.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                ActiveCards[i] = EditorGUILayout.Foldout(ActiveCards[i], faction.Cards[i].Name);
                EditorGUILayout.EndHorizontal();

                if (ActiveCards[i])
                {
                    EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

                    GUILayout.Space(40); //Tab in 20 pixels //https://forum.unity.com/threads/indenting-guilayout-objects.113494/

                    EditorGUILayout.BeginVertical(); //Vertical contains ints
                    if (faction.Cards[i].Special)
                    {
                        SpecialCardEditor.DrawInspector((SpecialCard)faction.Cards[i]);
                    }
                    else
                    {
                        UnitCardEditor.DrawInspector((UnitCard)faction.Cards[i]);
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

}
