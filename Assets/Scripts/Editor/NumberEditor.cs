using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Number))]
public class NumberEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //分隔线
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("数字管理", EditorStyles.boldLabel); 


        Number number = (Number)target;
        EditorGUILayout.BeginHorizontal();
        {
            number.value = EditorGUILayout.LongField("输入数字", number.value);

            if (GUILayout.Button("显示输入的数字"))
            {
                number.DisplayNumber(number.value);
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        {
            number.value = EditorGUILayout.LongField("输入百分数", number.value);

            if (GUILayout.Button("显示输入的百分数"))
            {
                number.DisplayNumberPercent(number.value);
            }
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("显示50000"))
        {
            number.DisplayNumber(50000);
        }
    }
}