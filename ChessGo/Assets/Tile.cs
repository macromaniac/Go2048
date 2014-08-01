using System;
using System.Collections.Generic;
using UnityEngine;



public class Tile {
	TileType tileType;
	Vector2D startingPos;
	public Tile(TileType tileType, Vector2D startingPos) {
		this.tileType = tileType;
		this.startingPos = startingPos;
	}
	public Tile(TileType tileType, int x, int y) {
		this.tileType = tileType;
		this.startingPos = new Vector2D(x, y);
	}

	public TileType TileType {
		get {
			return tileType;
		}
	}

	public override string ToString() {
		return tileType.ToStringRepresentation() + startingPos.ToString();
	}
}

