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
        base.OnInspectorGUI();
        
        //zone.Type = (Zone.Types)EditorGUILayout.EnumPopup("Type", zone.Type);
        EditorGUILayout.EnumPopup("Type", zone.Type);

        cards = EditorGUILayout.Foldout(cards, "Cards");
        if (zone.Cards != null && cards)
        {
            if (active == null)
            {
                active = new bool[zone.Cards.Count];
                for (int i = 0; i < active.Length; i++)
                {
                    active[i] = false;
                }
            }
            for (int i = 0; i < zone.Cards.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(20);
                active[i] = EditorGUILayout.Foldout(active[i], zone.Cards[i].Name);
                EditorGUILayout.EndHorizontal();

                if (active[i])
                {
                    EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

                    GUILayout.Space(40); //Tab in 20 pixels //https://forum.unity.com/threads/indenting-guilayout-objects.113494/

                    EditorGUILayout.BeginVertical(); //Vertical contains ints
                    UnitCardEditor.DrawInspector(zone.Cards[i]);
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.EndHorizontal();
                }
            }
        }
    }

}
