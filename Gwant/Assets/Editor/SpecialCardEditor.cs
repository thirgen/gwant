using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpecialCard))]
public class SpecialCardEditor : Editor {

    public override void OnInspectorGUI()
    {
        SpecialCard card = ((GameObject)target).GetComponent<SpecialCard>();
        //SpecialCard card = (SpecialCard)target;
        base.OnInspectorGUI();
        DrawInspector(card);
    }

    public static void DrawInspector(SpecialCard card)
    {

        EditorGUILayout.LabelField("ID", card.ID.ToString());
        EditorGUILayout.LabelField("Name", card.Name);
        EditorGUILayout.LabelField("Art", card.Art);
        EditorGUILayout.EnumPopup("Ability", card.Ability);
        if (card.Ability == Card.Abilities.Weather)
            EditorGUILayout.EnumPopup("Weather", card.WeatherType);
    }
}
