using System;
using System.Collections.Generic;

using UnityEngine;

public class Tile {
	public Point2D pos;
	TileType tileType;
	public Tile(Point2D pos, TileType tileType) {
		this.pos = pos;
		this.tileType = tileType;
	}
	public Tile(Point2D pos, char tileChar) {
		this.pos = pos;
		this.tileType = Util.charToTileType(tileChar);
	}

	public string getChar() {
		return Util.tileTypeToChar(tileType).ToString();
	}
}
