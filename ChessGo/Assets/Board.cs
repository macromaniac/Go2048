using System.Collections;
using System.Collections.Generic;

public class Board {
	List<List<Tile>> board;
	public Board(string rawMap) {
		board = new List<List<Tile>>();

		string[] mapRows = rawMap.Split('\n');

		for (int i = 0; i < mapRows.Length; ++i) {
			board.Add(new List<Tile>());
			int y = mapRows.Length - i - 1;
			for (int x = 0; x < mapRows[i].Length; ++x)
				board[i].Add(new Tile(mapRows[i][x].ToTileType(), x, y));
		}
	}

	public override string ToString() {
		string toReturn="";
		foreach(List<Tile> row in board){
			foreach (Tile tile in row)
				toReturn += tile.ToString();
			toReturn+="\n";
		}
		toReturn = toReturn.TrimEnd('\n');
		return toReturn;
	}

	public string ToPrettyString() {
		string toReturn="";
		foreach(List<Tile> row in board){
			foreach (Tile tile in row) {
				if (tile.ToString() == "x")
					toReturn += " ";
				else
					toReturn += tile.ToString();
				toReturn += "\t";
			}
			toReturn+="\n";
		}
		toReturn = toReturn.TrimEnd('\n');
		return toReturn;
	}

}