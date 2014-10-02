using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Map))]
public class MapInspector : Editor {
	
	public override void OnInspectorGUI() {
		//base.OnInspectorGUI();
		DrawDefaultInspector();
		if (GUILayout.Button("Regenerate")) {
			Map map = (Map) target;
//			tileMap.BuildMesh();
		}
	}
}