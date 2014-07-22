using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right, None };
public enum TileType { White, WhiteKing, Black, BlackKing, Empty, Wall };
public enum PlayerColor { White, Black, None };

public class Point2D {
	public int x, y;
	public Point2D(int x, int y) {
		this.x = x; this.y = y;
	}
	public static Point2D operator +(Point2D p1, Point2D p2) {
		return new Point2D(p1.x + p2.x, p1.y + p2.y);
	}
	public static Point2D operator -(Point2D p1, Point2D p2) {
		return new Point2D(p1.x - p2.x, p1.y - p2.y);
	}
	public static Point2D operator *(Point2D p1, int i1) {
		return new Point2D(p1.x * i1, p1.y * i1);
	}
	public static Point2D operator *(int i1, Point2D p1) {
		return new Point2D(p1.x * i1, p1.y * i1);
	}
	public override string ToString() {
		return "(" + x + "," + y + ")";
	}

	public bool isEqualTo(Point2D p) {
		return (x == p.x && y == p.y);
	}

}
public static class Conversions {
	public static Point2D ToPoint2D(this Direction direction) {
		if (direction == Direction.Up)
			return new Point2D(0, 1);
		if (direction == Direction.Down)
			return new Point2D(0, -1);
		if (direction == Direction.Left)
			return new Point2D(-1, 0);
		if (direction == Direction.Right)
			return new Point2D(1, 0);
		return new Point2D(0, 0);
	}
	public static PlayerColor ToPlayerColor(this int playerNumber) {
		if (playerNumber == 0)
			return PlayerColor.White;
		if (playerNumber == 1)
			return PlayerColor.Black;
		return PlayerColor.None;
	}

	public static TileType ToTileType(this char input) {
		if (input == 'B')
			return TileType.BlackKing;
		if (input == 'W')
			return TileType.WhiteKing;
		if (input == 'w')
			return TileType.White;
		if (input == 'b')
			return TileType.Black;
		if (input == '.')
			return TileType.Empty;
		int a = 0;
		return TileType.Wall;
	}
	public static char ToChar(this TileType tileType) {
		if (tileType == TileType.BlackKing)
			return 'B';
		if (tileType == TileType.WhiteKing)
			return 'W';
		if (tileType == TileType.White)
			return 'w';
		if (tileType == TileType.Black)
			return 'b';
		if (tileType == TileType.Empty)
			return '.';
		return 'x';
	}
	public static PlayerColor ToPlayerColor(this TileType tileType) {
		if (tileType == TileType.Black || tileType == TileType.BlackKing)
			return PlayerColor.Black;
		if (tileType == TileType.White || tileType == TileType.WhiteKing)
			return PlayerColor.White;
		return PlayerColor.None;
	}

}
public static class Util {
	public static bool isMovableType(this TileType tileType) {
		if (tileType.ToPlayerColor() != PlayerColor.None)
			return true;
		return false;
	}
	public static bool isKing(this TileType tileType) {
		if (tileType == TileType.BlackKing || tileType == TileType.WhiteKing)
			return true;
		return false;
	}
//	public static string startingMap =
//@"" +
//"xxxxxxxx\n" +
//"xxxxxxxx\n" +
//"xxw....x\n" +
//"xx.W...x\n" +
//"xx.....x\n" +
//"xx...B.x\n" +
//"xx....bx\n" +
//"xxxxxxxx\n" +
//"xxxxxxxx";


	public static string startingMap =
@"" +
"xxxxxxxxx\n" +
"xb.....wx\n" +
"x.......x\n" +
"x..W....x\n" +
"x.......x\n" +
"x....B..x\n" +
"x.......x\n" +
"xb.....wx\n" +
"xxxxxxxxx";
}
