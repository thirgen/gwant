using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardGO))]
public class CardGOEditor : Editor
{
    static bool cards = false;
    bool[] active;

    public override void OnInspectorGUI()
    {
        CardGO cardGO = (CardGO)target;
        //base.OnInspectorGUI();

        SerializedProperty p = serializedObject.FindProperty("text");
        EditorGUILayout.PropertyField(p);

        //EditorGUILayout.LabelField("Type", zone.Type.ToString().ToUpper());

        //zone.Type = (Zone.Types)EditorGUILayout.EnumPopup("Type", zone.Type);


        //EditorGUILayout.DropdownButton(GUIContent.none, FocusType.Passive);
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("Cards"));
        //cards = EditorGUILayout.Toggle("Expand Cards", cards);
        cards = EditorGUILayout.Foldout(cards, "Cards");
        if (cards)
        {
            if (active == null)
            {
                active = new bool[cardGO.cards.Count];
                for (int i = 0; i < active.Length; i++)
                {
                    active[i] = false;
                }
            }
            for (int i = 0; i < cardGO.cards.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                active[i] = EditorGUILayout.Foldout(active[i], cardGO.cards[i].Name);
                EditorGUILayout.EndHorizontal();

                if (active[i])
                {
                    EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

                    GUILayout.Space(40); //Tab in 20 pixels //https://forum.unity.com/threads/indenting-guilayout-objects.113494/

                    EditorGUILayout.BeginVertical(); //Vertical contains ints
                    UnitCardEditor.DrawInspector((UnitCard)cardGO.cards[i]);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

}
