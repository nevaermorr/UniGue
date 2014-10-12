using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
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
	
	public void Start () {
		Generate();
	}
	
	
	public void Generate() {
		CreateTiles();
		BuildMesh();
	}
	
	private void CreateTiles() {
		tiles = MapGenerator.GenerateMap();	
	}
	
	public void BuildMesh() {
		// Number of vertices
		int numVertices = sizeX * sizeZ * 6;
		
		// Generate mesh data
		Vector3[] vertices = new Vector3[numVertices];
		Vector3[] normals = new Vector3[numVertices];
		Vector2[] uv = new Vector2[numVertices];
		int[] triangles = new int[numVertices];
		
		// Tile's number in sequence
		int tileNumber = 0;
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				// For each tile
				
				// Create vertices for both triangles
				vertices[6 * tileNumber    ] = new Vector3( x      * tileSize, 0,  z      * tileSize);
				vertices[6 * tileNumber + 1] = new Vector3( x      * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 2] = new Vector3((x + 1) * tileSize, 0,  z      * tileSize);
				
				vertices[6 * tileNumber + 3] = new Vector3( x      * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 4] = new Vector3((x + 1) * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 5] = new Vector3((x + 1) * tileSize, 0,  z      * tileSize);
				
				// Create triangles
				for (int i = 0; i < 6; i++) {
					triangles[6 * tileNumber + i] = 6 * tileNumber + i;
				}
				
				// Create normals
				for (int i = 0; i < 6; i++) {
					normals[6 * tileNumber + i] = Vector3.up;
				}
				
				Vector2[] uvCorners = _tiles[x, z].getTextureUVCorners();
				
				//create uvs
				uv[6 * tileNumber    ] = uvCorners[0];
				uv[6 * tileNumber + 1] = new Vector2(uvCorners[0].x, uvCorners[1].y);
				uv[6 * tileNumber + 2] = new Vector2(uvCorners[1].x, uvCorners[0].y);
				uv[6 * tileNumber + 3] = new Vector2(uvCorners[0].x, uvCorners[1].y);
				uv[6 * tileNumber + 4] = uvCorners[1];
				uv[6 * tileNumber + 5] = new Vector2(uvCorners[1].x, uvCorners[0].y);
				
				// Move along to next tile
				tileNumber++;
			}
		}
		
		// Create new mesh and populate with data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		// Assign mesh
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();
		
		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
	}
}
