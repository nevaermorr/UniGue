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
	
	private Tile[,] tiles;
	
	// Use this for initialization
	public void Start () {
		CreateTiles();
		LoadTileTexture();
		BuildMesh();
	}
	
	private void CreateTiles() {
		tiles = new Tile[sizeX, sizeZ];
		
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				tiles[x, z] = new Tile(new Vector2(x, z));
			}
		}
	}
	
	public void BuildMesh() {
		// number of vertices
		int numVertices = sizeX * sizeZ * 6;
		
		// generate mesh data
		Vector3[] vertices = new Vector3[numVertices];
		Vector3[] normals = new Vector3[numVertices];
		Vector2[] uv = new Vector2[numVertices];
		int[] triangles = new int[numVertices];
		
		// tile's number in sequence
		int tileNumber = 0;
		for (int x = 0; x < sizeX; x++) {
			for (int z = 0; z < sizeZ; z++) {
				// for each tile
				
				// create vertices for both triangles
				vertices[6 * tileNumber    ] = new Vector3( x      * tileSize, 0,  z      * tileSize);
				vertices[6 * tileNumber + 1] = new Vector3( x      * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 2] = new Vector3((x + 1) * tileSize, 0,  z      * tileSize);
				
				vertices[6 * tileNumber + 3] = new Vector3( x      * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 4] = new Vector3((x + 1) * tileSize, 0, (z + 1) * tileSize);
				vertices[6 * tileNumber + 5] = new Vector3((x + 1) * tileSize, 0,  z      * tileSize);
				
				// create triangles
				for (int i = 0; i < 6; i++) {
					triangles[6 * tileNumber + i] = 6 * tileNumber + i;
				}
				
				//create normals
				for (int i = 0; i < 6; i++) {
					normals[6 * tileNumber + i] = Vector3.up;
				}
				
				Vector2[] uvCorners = tiles[x, z].getUVCorners();
				
				//create uvs
				uv[6 * tileNumber    ] = uvCorners[0];
				uv[6 * tileNumber + 1] = new Vector2(uvCorners[0].x, uvCorners[1].y);
				uv[6 * tileNumber + 2] = new Vector2(uvCorners[1].x, uvCorners[0].y);
				uv[6 * tileNumber + 3] = new Vector2(uvCorners[0].x, uvCorners[1].y);
				uv[6 * tileNumber + 4] = uvCorners[1];
				uv[6 * tileNumber + 5] = new Vector2(uvCorners[1].x, uvCorners[0].y);
				
				// move along to next tile
				tileNumber++;
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
