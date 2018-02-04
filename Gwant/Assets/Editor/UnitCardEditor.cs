using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnitCard))]
public class UnitCardEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawInspector((UnitCard)target);

    }

    public static void DrawInspector(UnitCard card)
    {
        //UnitCard card = (UnitCard)target;

        //base.OnInspectorGUI();
        
        EditorGUILayout.LabelField("ID", card.ID.ToString());
        EditorGUILayout.LabelField("Name", card.Name);
        EditorGUILayout.LabelField("Art", card.Art);
        //EditorGUILayout.LabelField("Special", card.Special.ToString());
        EditorGUILayout.EnumPopup("Section", card.Section);
        EditorGUILayout.Toggle("Hero", card.Hero);
        EditorGUILayout.EnumPopup("Ability", card.Ability);
        if (card.Ability == Card.Abilities.Avenger)
        {
            EditorGUILayout.IntField("Avenger", card.Avenger);
        }
        else if (card.Ability == Card.Abilities.Muster)
            EditorGUILayout.LabelField("Muster", card.Muster);
        else if (card.Ability == Card.Abilities.Scorch)
        {
            EditorGUILayout.IntField("Scorch Value", card.ScorchThreshold);
            //EditorGUILayout.HelpBox("The value that a row must be greater than for the Scorch ability to activate", MessageType.Info);
        }
        else if (card.Ability == Card.Abilities.Bond)
            EditorGUILayout.Toggle("Bond", card.Bond);

        if (!card.Hero)
        {
            GUILayout.Label("Strength");
            //  ----------------------------
            //  | space | Total [Strength] |
            //  | space | Base  [Strength] |
            //  ----------------------------
            EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

            GUILayout.Space(20); //Tab in 20 pixels //https://forum.unity.com/threads/indenting-guilayout-objects.113494/

            EditorGUILayout.BeginVertical(); //Vertical contains ints
            EditorGUILayout.IntField("Total", card.Strength);
            EditorGUILayout.IntField("Base", card.GetBaseStrength());
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }
        else
            EditorGUILayout.IntField("Strength", card.Strength);
        if (card.Ability == Card.Abilities.Morale)
        {
            GUILayout.Label("Morale");
            EditorGUILayout.BeginHorizontal(); //Horizontal contains tab + ints

            GUILayout.Space(20); //Tab in 20 pixels

            EditorGUILayout.BeginVertical(); //Vertical contains ints
            EditorGUILayout.IntField("Total", card.Morale);
            EditorGUILayout.IntField("Base", card.GetBaseMorale());
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }
        else if (!card.Hero)
        {
            EditorGUILayout.IntField("Morale", card.Morale);
            EditorGUILayout.Toggle("Horn", card.Horn);
        }

        //EditorGUILayout.Vector2IntField("Strength", new Vector2Int(card.Strength, card.GetBaseStrength()));
        //EditorGUILayout.Vector2IntField("Morale", new Vector2Int(card.Morale, card.GetBaseMorale()));

        //EditorGUILayout.
    }

}
