using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {
	
	// possible types of tile
	public enum types {
		none,
		stone,
		wall,
		floor,
	};

	// The physical embodiment of the tile
	public GameObject structure;

	// type of this particular tile
	private int _type;
	public int type {
		get {
			return _type;
		}
		set {
			_type = value;
			
		}
	}
	
	private void CreateStructure() {
		switch(type) {
			case (int)types.floor:
				structure = Instantiate(Resources.Load<GameObject>("Map/Floor")) as GameObject;
				structure.name = "floor";
				break;
			case (int)types.wall:
				structure = Instantiate(Resources.Load<GameObject>("Map/Wall")) as GameObject;
				structure.name = "wall";
				break;
		}
		if (structure != null) {
			structure.transform.parent = gameObject.transform;
			// Treat position from editor as relative
			structure.transform.localPosition = structure.transform.position;
		}
	}
	
	void Start () {
		CreateStructure();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
