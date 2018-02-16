using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardEventTrigger))]
public class CardEventTriggerEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        //CardEventTrigger trigger = (CardEventTrigger)target;
        CardEventTrigger.DefaultColour = EditorGUILayout.ColorField("Default Colour", CardEventTrigger.DefaultColour);
        CardEventTrigger.HighlightColour = EditorGUILayout.ColorField("Highlight Colour", CardEventTrigger.HighlightColour);
        CardEventTrigger.SelectedColour = EditorGUILayout.ColorField("Selected Colour", CardEventTrigger.SelectedColour);

    }

}
