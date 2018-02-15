using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardGO))]
public class CardGOEditor : Editor {

    public override void OnInspectorGUI()
    {

        CardEventTrigger trigger = ((CardGO)target).GetComponent<CardEventTrigger>();
        trigger.DefaultColour = EditorGUILayout.ColorField("Default Colour", trigger.DefaultColour);
        trigger.HighlightColour = EditorGUILayout.ColorField("Highlight Colour", trigger.HighlightColour);
        trigger.SelectedColour = EditorGUILayout.ColorField("Selected Colour", trigger.SelectedColour);

        base.OnInspectorGUI();


        Card card = ((CardGO)target).Card;
        if (card != null)
        {
            if (card.Special)
                SpecialCardEditor.DrawInspector((SpecialCard)card);
            else
                UnitCardEditor.DrawInspector((UnitCard)card);
        }
    }

}
