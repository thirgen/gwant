using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardGO))]
public class CardGOEditor : Editor {

    public override void OnInspectorGUI()
    {
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
