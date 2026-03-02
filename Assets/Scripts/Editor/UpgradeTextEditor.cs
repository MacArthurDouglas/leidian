using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeText))]
public class UpgradeTextEditor : Editor
{
    /*public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UpgradeText upgradeText = (UpgradeText)target;
        EditorGUILayout.BeginHorizontal();
        {
            upgradeText.CurrentTextValue = (UpgradeText.TextValue)EditorGUILayout.EnumPopup("当前图片", upgradeText.CurrentTextValue);
        
            if (GUILayout.Button("切换图片"))
            {
                upgradeText.DisplayText(upgradeText.CurrentTextValue);
            }
        }
        EditorGUILayout.EndHorizontal();
    }*/
}