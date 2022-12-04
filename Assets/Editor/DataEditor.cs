using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeteoriteCurve))]
public class DataEditor : Editor
{
    Vector2 scroll;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MeteoriteCurve data = (MeteoriteCurve)target;

        if (data.SpawnsPerStage == null || data.Meteorite == null)
            return;
        scroll = EditorGUILayout.BeginScrollView(scroll);

        // The outer box
        EditorGUILayout.BeginHorizontal("box");
        EditorGUIUtility.fieldWidth = 10;

        // Descriptors
        EditorGUILayout.BeginVertical("box");
        EditorGUIUtility.labelWidth = 40;
        EditorGUILayout.LabelField("Stages: ");
        EditorGUILayout.LabelField("Spawns: ");
        EditorGUILayout.LabelField("Time: ");
        EditorGUILayout.LabelField("Const: ");
        EditorGUILayout.EndVertical();
        // Inner 
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter};
        for (int stage = 1; stage <= 100; stage++)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUIUtility.labelWidth = 15;
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(stage.ToString(), style);
            float newValue = EditorGUILayout.FloatField(data.GetSpawns(stage), style);
            if (newValue != data.GetSpawns(stage))
            {
                int index = data.SpawnsPerStage.AddKey(stage, newValue);
                // at the spot is another keyframe
                if (index == -1)
                {
                    // check all keyframes find the one we "hit"
                    for (int key = 0; key < data.SpawnsPerStage.length; key++)
                    {
                        if (data.SpawnsPerStage.keys[key].time == stage)
                        {
                            // replace it
                            data.SpawnsPerStage.MoveKey(key, new Keyframe(stage, newValue));
                        }
                    }
                }
            }
            EditorGUILayout.LabelField(string.Format("{0:0.00}", data.GetTimeBetweenSpawns(stage)), style);
            if (GUILayout.Button(""))
            {
                // check all keyframes find the one we "hit"
                for (int key = 0; key < data.SpawnsPerStage.length; key++)
                {
                    if (data.SpawnsPerStage.keys[key].time == stage)
                    {
                        // set constant
                        AnimationUtility.SetKeyLeftTangentMode(data.SpawnsPerStage, key, AnimationUtility.TangentMode.Constant);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }
}
