using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardEventTrigger))]
public class CardEventTriggerEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        CardEventTrigger.DefaultColour = EditorGUILayout.ColorField("Default Colour", CardEventTrigger.DefaultColour);
        CardEventTrigger.HighlightColour = EditorGUILayout.ColorField("Highlight Colour", CardEventTrigger.HighlightColour);
        CardEventTrigger.SelectedColour = EditorGUILayout.ColorField("Selected Colour", CardEventTrigger.SelectedColour);

        CardEventTrigger trigger = (CardEventTrigger)target;

        //SerializedProperty strengthSprites = serializedObject.FindProperty("StrengthImages");
        //EditorGUILayout.PropertyField(strengthSprites);

        EditorGUILayout.Toggle("Highlighted", trigger.Highlighted);
        EditorGUILayout.Toggle("Special Highlighted", trigger.SpecialHighlighted);
    }

}
