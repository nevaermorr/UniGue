using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public int sizeX;
	public int sizeZ;
	
	public float tileSize = 1.0f; 
	public int tileResolution;
	
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
	
	public GameObject tilePrefab;
	public GameObject test;
	
	public void Start () {
		Generate();
	}
	
	public void Generate() {
		DestroyTiles();
		CreateTiles();
	}
	
	private void CreateTiles() {
//		tiles = MapGenerator.GenerateMap();	
		for (float x=0; x<sizeX; x++) {
			for (float z=0; z<sizeZ; z++) {
				GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0f, z), Quaternion.identity)
					as GameObject;
				tile.name = "Tile [" + x.ToString() + "," + z.ToString() + "]";
				
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
