using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { up, down, left, right };
public enum TileType { white, whiteKing, black, blackKing, empty, wall };

public class Point2D {
	public int x, y;
	public Point2D(int x, int y) {
		this.x = x; this.y = y;
	}
	public static Point2D operator +(Point2D p1, Point2D p2) {
		return new Point2D(p1.x + p2.x, p1.y + p2.y);
	}

}
public class Util {
	public static Point2D singleStep(Direction direction) {
		if (direction == Direction.up)
			return new Point2D(0, 1);
		if (direction == Direction.down)
			return new Point2D(0, -1);
		if (direction == Direction.left)
			return new Point2D(-1, 0);
		if (direction == Direction.right)
			return new Point2D(0, 1);
		return new Point2D(0, 0);
	}
	public static TileType charToTileType(char input) {
		if (input == 'B')
			return TileType.blackKing;
		if (input == 'W')
			return TileType.whiteKing;
		if (input == 'w')
			return TileType.white;
		if (input == 'b')
			return TileType.black;
		if (input == '.')
			return TileType.empty;
		return TileType.wall;
	}
	public static char tileTypeToChar(TileType tileType) {
		if (tileType == TileType.blackKing)
			return 'B';
		if (tileType == TileType.whiteKing)
			return 'W';
		if (tileType == TileType.white)
			return 'w';
		if (tileType == TileType.black)
			return 'b';
		if (tileType == TileType.empty)
			return '.';
		return 'x';
	}
	public static string startingMap =
@"" +
"xxxxxxxx\n" +
"x..w...x\n" +
"x..w...x\n" +
"x..W...x\n" +
"x...B..x\n" +
"x......x\n" +
"x......x\n" +
"xxxxxxxx\n" +
"xxxxxxxx";

}
