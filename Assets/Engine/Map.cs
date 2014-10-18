using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public int sizeX;
	public int sizeZ;
	
	private Tile[,] _tiles;
	public Tile[,] tiles {
		get {
			return _tiles;
		}
		set {
			_tiles = value;
			// Make sure that size of the map is updated
			sizeX = _tiles.GetLength(0);
			sizeZ = _tiles.GetLength(1);
		}
	}
	
	public void Start () {
		Generate();
	}
	
	private void LoadPrefabs() {
	}
	
	public void Generate() {
		// Dispose of unwanted tiles
		DestroyTiles();
		CreateTiles();
	}
	
	private void CreateTiles() {
//		tiles = MapGenerator.GenerateMap();	
		for (float x=0; x<sizeX; x++) {
			for (float z=0; z<sizeZ; z++) {
				GameObject tile = Instantiate(Resources.Load<GameObject>("Map/Tile"))
					as GameObject;
				tile.name = "Tile [" + x.ToString() + "," + z.ToString() + "]";
				tile.transform.position = new Vector3(x, 0f, z);
				
				Tile tileScript = tile.GetComponent<Tile>();
				tileScript.type = (int)Tile.types.floor;
				// Build wall
				if (x == 0 || z == 0 || x == sizeX-1 || z == sizeZ-1) {
					tileScript.type = (int)Tile.types.wall;
				}
				
				// Place properly in the hierarchy
				tile.transform.parent = gameObject.transform;
			}
		}
	}
	
	private void DestroyTiles() {
		foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile")) {
			GameObject.DestroyImmediate(tile);
		}
	}
	
}
