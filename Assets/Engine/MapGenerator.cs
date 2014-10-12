using UnityEngine;
using System.Collections;

public class MapGenerator {
	public static Tile[,] GenerateMap(string id = "") {	
		return GenerateMapTest();
		// TODO add some means of choice here
	}
	
	private static Tile[,] GenerateMapTest() {
		int sizeX = 30;
		int sizeZ = 20;
		
		Tile[,] tiles = new Tile[sizeX, sizeZ];
		// Initialize background
		for (int x=0; x<sizeX; x++) {
			for (int z=0; z<sizeZ; z++) {
				tiles[x, z] = new Tile();
			}
		}
		
		AddRoomToTiles(tiles, new IntVector2(2, 3), new IntVector2(5, 7));
		
		return tiles;
	}
	private static void AddRoomToTiles (Tile[,] tiles, IntVector2 bottomLeft, IntVector2 topRight) {
		
		// The walls of a room
		foreach(IntVector2 rectanglePoint in RectanglePeripheryGenerator(bottomLeft, topRight)) {
			tiles[rectanglePoint.x, rectanglePoint.y].type = (int)Tile.types.wall;
		}
		// The interior of the room
		foreach(IntVector2 rectanglePoint in RectangleAreaGenerator(bottomLeft.Shift(1,1), topRight.Shift(-1,-1))) {
			tiles[rectanglePoint.x, rectanglePoint.y].type = (int)Tile.types.floor;
		}
	}
	
	private static IEnumerable RectanglePeripheryGenerator(IntVector2 bottomLeft, IntVector2 topRight) {
	
		// Validate rectangle corners
		if (
			bottomLeft.x >= topRight.x
			|| bottomLeft.y >= topRight.y
		) {
			Log.Warning("Invalid rectangle corners");
			yield break;
		}
	
		for (int x=bottomLeft.x; x<=topRight.x; x++) {
			// Bottom line
			yield return new IntVector2(x, bottomLeft.y);
			// Top line
			yield return new IntVector2(x, topRight.y);
		}
		
		for (int z = bottomLeft.y+1; z<topRight.y; z++) {
			// Left line
			yield return new IntVector2(bottomLeft.x, z);
			// Right line
			yield return new IntVector2(topRight.x, z);
		}
	}
	
	private static IEnumerable RectangleAreaGenerator(IntVector2 bottomLeft, IntVector2 topRight) {
		// Validate rectangle corners
		if (
			bottomLeft.x > topRight.x
			|| bottomLeft.y > topRight.y
			) {
			Log.Warning("Invalid rectangle corners");
			yield break;
		}
		
		for (int x=bottomLeft.x; x<=topRight.x; x++) {
			for (int z=bottomLeft.y; z<=topRight.y; z++) {
				yield return new IntVector2(x, z);
			}
		}
	}
}