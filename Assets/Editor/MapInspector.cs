using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Map))]
public class MapInspector : Editor {
	
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		// Add button for re-genarating map
		if (GUILayout.Button("Re-generate")) {
			Map map = (Map) target;
			map.Generate();
		}
	}
}