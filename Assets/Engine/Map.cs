using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class Map : MonoBehaviour {

	public int sizeX = 15;
	public int sizeZ = 10;

	public float tileSize = 1.0f; 
	public int tileResolution;
	
	private Tile[] tiles;
	
	// Use this for initialization
	void Start () {
		BuildMesh();
	}
	
	public void BuildMesh() {
		
		// number of tiles on the map
		int numTiles = sizeX * sizeZ;
		// number of triangles needed to form tiles
		int numTriangles = numTiles * 2;
		
		// number of vertices
		int numVertices = sizeX * sizeZ * 6;
		
		Tile[] tiles = new Tile[numTiles];
		
		// generate mesh data
		Vector3[] vertices = new Vector3[numVertices];
		Vector3[] normals = new Vector3[numVertices];
		Vector2[] uv = new Vector2[numVertices];
		
		int[] triangles = new int[numTriangles * 3];
		
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				// for each tile
				
				// tile's number in sequence
				int tileNumber = XZToN(x, z);
				
				// create vertices for both triangles
				vertices[6 * tileNumber    ] = new Vector3( x      * tileSize, 0,  z      * tileSize);
				vertices[6 * tileNumber + 1] = new Vector3( x      * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 2] = new Vector3((x + 1) * tileSize, 0,  z      * tileSize);
				
				vertices[6 * tileNumber + 3] = new Vector3( x      * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 4] = new Vector3((x + 1) * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 5] = new Vector3((x + 1) * tileSize, 0,  z      * tileSize);
				
				tiles[tileNumber] = new Tile();
				
				
				// create triangles
				for (int i = 0; i < 6; i++) {
					triangles[6 * tileNumber + i] = 6 * tileNumber + i;
				}
				
				//create normals
				for (int i = 0; i < 6; i++) {
					normals[6 * tileNumber + i] = Vector3.up;
				}
				
				float[,] uvCorners = tiles[tileNumber].getUVCorners();
				
				//create uvs
				uv[6 * tileNumber    ] = new Vector2(uvCorners[0, 0], uvCorners[0, 1]);
				uv[6 * tileNumber + 1] = new Vector2(uvCorners[0, 0], uvCorners[1, 1]);
				uv[6 * tileNumber + 2] = new Vector2(uvCorners[1, 0], uvCorners[0, 1]);
				uv[6 * tileNumber + 3] = new Vector2(uvCorners[0, 0], uvCorners[1, 1]);
				uv[6 * tileNumber + 4] = new Vector2(uvCorners[1, 0], uvCorners[1, 1]);
				uv[6 * tileNumber + 5] = new Vector2(uvCorners[1, 0], uvCorners[0, 1]);
			}
		}
		
		// create new mesh and populate with data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		// assign mesh
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();
		
		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
		
		LoadTileTexture();
	}
	
	void LoadTileTexture() {
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterials[0].mainTexture = Tile.texture;
	}
	
	// get number in sequence for element [x, z] in matrix
	int XZToN(int x, int z) {
		return z * sizeX + x;
	}
}
