﻿using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AlpacaSound
{
	[CustomEditor (typeof (RetroPixel))]
	public class RetroPixelEditor : Editor
	{
		SerializedObject serObj;

		SerializedProperty horizontalResolution;
		SerializedProperty verticalResolution;
		SerializedProperty numColors;

		SerializedProperty color0;
		SerializedProperty color1;
		SerializedProperty color2;
		SerializedProperty color3;
		SerializedProperty color4;
		SerializedProperty color5;
		SerializedProperty color6;
		SerializedProperty color7;

		void OnEnable ()
		{
			serObj = new SerializedObject (target);
			
			horizontalResolution = serObj.FindProperty ("horizontalResolution");
			verticalResolution = serObj.FindProperty ("verticalResolution");
			color0 = serObj.FindProperty ("color0");
			color1 = serObj.FindProperty ("color1");
			color2 = serObj.FindProperty ("color2");
			color3 = serObj.FindProperty ("color3");
			color4 = serObj.FindProperty ("color4");
			color5 = serObj.FindProperty ("color5");
			color6 = serObj.FindProperty ("color6");
			color7 = serObj.FindProperty ("color7");
		}

		override public void OnInspectorGUI ()
		{
			serObj.Update ();

			//RetroPixel myTarget = (RetroPixel) target;

			horizontalResolution.intValue = EditorGUILayout.IntField("Horizontal Resolution", horizontalResolution.intValue);
			verticalResolution.intValue = EditorGUILayout.IntField("Vertical Resolution", verticalResolution.intValue);
			
			 color0.colorValue = EditorGUILayout.ColorField("Color 0", color0.colorValue);
			 color1.colorValue = EditorGUILayout.ColorField("Color 1", color1.colorValue);
		     color2.colorValue = EditorGUILayout.ColorField("Color 2", color2.colorValue);
			 color3.colorValue = EditorGUILayout.ColorField("Color 3", color3.colorValue);
			 color4.colorValue = EditorGUILayout.ColorField("Color 4", color4.colorValue);
			 color5.colorValue = EditorGUILayout.ColorField("Color 5", color5.colorValue);
			 color7.colorValue = EditorGUILayout.ColorField("Color 7", color7.colorValue);
			 color6.colorValue = EditorGUILayout.ColorField("Color 6", color6.colorValue);

			serObj.ApplyModifiedProperties ();
		}
	}
}
