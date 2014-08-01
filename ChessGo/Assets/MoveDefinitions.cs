using System;
using System.Collections.Generic;

public enum Move {

	nE = 0,
	nW,
	Ne,
	Nw,

	eN,
	eS,
	En,
	Es,

	sE,
	sW,
	Se,
	Sw,

	wN,
	wS,
	Wn,
	Ws
}

public static class MoveTranslations {
	public static Vector2D[,] Translations = {
		{ new Vector2D(0, 1),  new Vector2D(2, 0) },
		{ new Vector2D(0, 1),  new Vector2D(-2, 0) },
		{ new Vector2D(0, 2),  new Vector2D(1, 0) },
		{ new Vector2D(0, 2),  new Vector2D(-1, 0) },

		{ new Vector2D(1, 0),  new Vector2D(0, 2) },
		{ new Vector2D(1, 0),  new Vector2D(0, -2) },
		{ new Vector2D(2, 0),  new Vector2D(0, 1) },
		{ new Vector2D(2, 0),  new Vector2D(0, -1) },

		{ new Vector2D(0, -1),  new Vector2D(2, 0) },
		{ new Vector2D(0, -1),  new Vector2D(-2, 0) },
		{ new Vector2D(0, -2),  new Vector2D(1, 0) },
		{ new Vector2D(0, -2),  new Vector2D(-1, 0) },

		{ new Vector2D(-1, 0),  new Vector2D(0, 2) },
		{ new Vector2D(-1, 0),  new Vector2D(0, -2) },
		{ new Vector2D(-2, 0),  new Vector2D(0, 1) },
		{ new Vector2D(-2, 0),  new Vector2D(0, -1) },
	};
}

public static class MoveDefinitions {
	public static Vector2D[] ToMoveVectors(this Move move) {
		Vector2D[] toReturn = {
			MoveTranslations.Translations[0,(int)move],
			MoveTranslations.Translations[1,(int)move]
		};
		return toReturn;
	}
}

public class Vector2D {
	public int x, y;
	public Vector2D(int x, int y) {
		this.x = x; this.y = y;
	}
	public static Vector2D operator +(Vector2D p1, Vector2D p2) {
		return new Vector2D(p1.x + p2.x, p1.y + p2.y);
	}
	public static Vector2D operator -(Vector2D p1, Vector2D p2) {
		return new Vector2D(p1.x - p2.x, p1.y - p2.y);
	}
	public static Vector2D operator *(Vector2D p1, int i1) {
		return new Vector2D(p1.x * i1, p1.y * i1);
	}
	public static Vector2D operator *(int i1, Vector2D p1) {
		return new Vector2D(p1.x * i1, p1.y * i1);
	}
	public override string ToString() {
		return "(" + x + "," + y + ")";
	}

	public bool IsEqualTo(Vector2D p) {
		return (x == p.x && y == p.y);
	}

}
