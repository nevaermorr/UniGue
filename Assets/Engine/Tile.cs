using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// type of this particular tile
	private int _type;
	public int type {
		get {
			return _type;
		}
		set {
			// Casting to int just in case (so it is possible to pass enum)
			_type = (int)value;
		}
	}
	// possible types of tile
	public enum types {
		none,
		stone,
		wall,
		floor,
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
