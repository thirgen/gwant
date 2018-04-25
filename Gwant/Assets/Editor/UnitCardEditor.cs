using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnitCard))]
public class UnitCardEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UnitCard card = ((GameObject)target).GetComponent<UnitCard>();
        DrawInspector(card);

    }

    public static void DrawInspector(UnitCard card)
    {
        EditorGUILayout.LabelField("ID", card.ID.ToString());
        EditorGUILayout.LabelField("Name", card.Name);
        EditorGUILayout.LabelField("Art", card.Art);
        EditorGUILayout.LabelField("Section", card.Section.ToString());
        EditorGUILayout.LabelField("Hero", card.Hero.ToString());


        EditorGUILayout.LabelField("Ability", card.Ability.ToString());
        if (card.Ability == Card.Abilities.Avenger)
            EditorGUILayout.LabelField("Avenger", card.Avenger.ToString());
        else if (card.Ability == Card.Abilities.Muster)
            EditorGUILayout.LabelField("Muster", card.Muster);
        else if (card.Ability == Card.Abilities.Scorch)
        {
            EditorGUILayout.LabelField("Scorch Value", card.ScorchThreshold.ToString());
            //EditorGUILayout.HelpBox("The value that a row must be greater than for the Scorch ability to activate", MessageType.Info);
        }
        else if (card.Ability == Card.Abilities.Bond)
            EditorGUILayout.LabelField("Bond", card.Bond.ToString());
        EditorGUILayout.Toggle("Ability Used", card.AbilityUsed);

        if (card.Hero)
            EditorGUILayout.LabelField("Strength", card.Strength.ToString());
        else
        {
            GUILayout.Label("Strength");
            //  ----------------------------
            //  | space | Total [Strength] |
            //  | space | Base  [Strength] |
            //  ----------------------------
            EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

            GUILayout.Space(20); //Tab in 20 pixels //https://forum.unity.com/threads/indenting-guilayout-objects.113494/

            EditorGUILayout.BeginVertical(); //Vertical contains ints
            EditorGUILayout.LabelField("Total", card.Strength.ToString());
            EditorGUILayout.LabelField("Base", card.GetBaseStrength().ToString());
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("Morale", card.Morale.ToString());
            EditorGUILayout.Toggle("Horn", card.Horn);
        }


    }
}
