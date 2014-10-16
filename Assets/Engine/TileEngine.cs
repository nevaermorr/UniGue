//using UnityEngine;
//using System.Collections;
//using System;
//
//public class TileEngine {
//	
//	// type of this particular tile
//	private int _type;
//	public int type{
//		get {
//			return _type;
//		}
//		set {
//			_type = value;
//		}
//	}
//	// possible types of tile
//	public enum types {
//		none,
//		stone,
//		wall,
//		floor,
//	};
//
//	// constructor
//	public Tile(int type = (int)Tile.types.none){
////		type = UnityEngine.Random.Range(0, Enum.GetNames(typeof(types)).Length);
//		this.type = type;
//	}
//	
//	// get bottom-left and upper-right corner mapping uv from tiles texture set for this tile
//	public Vector2[] getTextureUVCorners() {
//	
//		Vector2[] corners = new Vector2[2];
//		// assume that all tiles from texture set are represented by enum types
//		// and are in the same order
//		corners[0] = new Vector2(
//			(float) type / (Enum.GetNames(typeof(types)).Length),
//			0f
//		);
//		corners[1] = new Vector2(
//			(float) (type + 1) / (Enum.GetNames(typeof(types)).Length),
//			1f
//		);
//		return corners;
//	}
//	
//}
