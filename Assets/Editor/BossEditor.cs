using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Boss))]
public class BossEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        Boss boss = (Boss)target;
        
        if( GUILayout.Button("ReloadConfig"))
        {
            boss.ReloadConfig();
        }
    }
}