using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Tile {
	public Point2D pos;
	TileType tileType;
	public Tile(Point2D pos, TileType tileType) {
		this.pos = pos;
		this.tileType = tileType;
	}
	public Tile(Point2D pos, char tileChar) {
		this.pos = pos;
		this.tileType = tileChar.ToTileType();
	}

	public string GetChar() {
		return tileType.ToChar().ToString();
	}

	public bool IsKing() {
		if (tileType == TileType.BlackKing || tileType == TileType.WhiteKing)
			return true;
		return false;
	}

	public PlayerColor GetColor() {
		return tileType.ToPlayerColor();
	}

	public TileType GetTileType() {
		return tileType;
	}
}
