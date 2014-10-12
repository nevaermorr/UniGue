using UnityEngine;
using System.Collections;

public class Log {

	public static void Error(string message) {
		Debug.LogError(message);
	}
	
	public static void Warning(string message) {
		Debug.LogWarning(message);
	}
}
