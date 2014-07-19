using System;
using System.Collections.Generic;
using UnityEngine;

public class Board {
	private List<List<Tile>> board;
	public Board(string rawBoard) {
		string[] mapLines = rawBoard.Split('\n');
		board = new List<List<Tile>>();

		for (int y = 0; y < mapLines.Length; ++y) {
			board.Add(new List<Tile>());
			List<Tile> lastRow = board[board.Count-1];
			for (int x = 0; x < mapLines[y].Length; ++x)
				lastRow.Add(new Tile(new Point2D(x, y), mapLines[y][x]));
		}
	}

	public void printBoardToConsole() {
			string line = "";
		for (int x = 0; x < board.Count; ++x) {
			for (int y = 0; y < board[x].Count ; ++y) {
				line += board[x][y].getChar();
			}
			line += "\n";
		}
		Debug.Log(line);
	}
}
