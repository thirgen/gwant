using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Manager))]
public class ManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Manager manager = (Manager)target;

        string turn = (manager.PlayerOnesTurn) ? "Player 1" : "Player 2";
        EditorGUILayout.LabelField("Turn", turn);
        if (GUILayout.Button("Change Turn"))
        {
            manager.ChangeTurn();
        }

    }

}
