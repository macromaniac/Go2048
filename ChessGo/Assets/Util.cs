using System;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {Nothing, Empty, White, WhiteKing, Black, BlackKing};
public static class Util {

	public static string ToStringRepresentation(this TileType tileType) {
		if (tileType == TileType.Nothing)
			return "x";
		if (tileType == TileType.Empty)
			return ".";
		if (tileType == TileType.White)
			return "w";
		if (tileType == TileType.WhiteKing)
			return "W";
		if (tileType == TileType.Black)
			return "b";
		if (tileType == TileType.BlackKing)
			return "B";
		throw new Exception("Could not convert TileType to String");
	}

	public static TileType ToTileType(this char tileTypeChar) {
		if (tileTypeChar == 'x')
			return TileType.Nothing;
		if (tileTypeChar == '.')
			return TileType.Empty;
		if (tileTypeChar == 'w')
			return TileType.White;
		if (tileTypeChar == 'W')
			return TileType.WhiteKing;
		if (tileTypeChar == 'b')
			return TileType.Black;
		if (tileTypeChar == 'B')
			return TileType.BlackKing;
		throw new Exception("Could not convert char \"" + tileTypeChar + "\" to tileType");
	}

	public static string rawMap =
	"xxxxxxxx\n" +
	"x......x\n" +
	"x.W....x\n" +
	"x......x\n" +
	"x......x\n" +
	"x....B.x\n" +
	"x......x\n" +
	"xxxxxxxx";

}
