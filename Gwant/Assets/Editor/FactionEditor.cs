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

        //SerializedProperty p = serializedObject.FindProperty("text");
        //EditorGUILayout.PropertyField(p);

        //EditorGUILayout.LabelField("Type", zone.Type.ToString().ToUpper());

        //zone.Type = (Zone.Types)EditorGUILayout.EnumPopup("Type", zone.Type);


        //EditorGUILayout.DropdownButton(GUIContent.none, FocusType.Passive);
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("Cards"));
        //cards = EditorGUILayout.Toggle("Expand Cards", cards);
        EditorGUILayout.LabelField("Name", faction.Name);
        EditorGUILayout.Toggle("Exclusive", faction.Exclusive);

        //ZoneEditor.DrawCardFoldout(faction.Cards, ViewCards, ActiveCards);

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
                    UnitCardEditor.DrawInspector((UnitCard)faction.Cards[i]);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

}
