using System.Collections;
using System.Collections.Generic;

public class BoardSpawner {
	Board board;
	public BoardSpawner(Board board) {
		this.board = board;
	}
	List<Point2D> whiteKingPositions, blackKingPositions;
	public void saveKingPositions() {
		whiteKingPositions = new List<Point2D>();
		blackKingPositions = new List<Point2D>();
		foreach (List<Tile> tileList in board.GetBoard())
			foreach (Tile tile in tileList)
				if (tile.GetTileType().isKing())
					if (tile.GetTileType().ToPlayerColor() == PlayerColor.White)
						whiteKingPositions.Add(tile.pos);
					else
						blackKingPositions.Add(tile.pos);
	}

	public void trySpawningOnKingPositions() {
		foreach (Point2D whiteKingPoint in whiteKingPositions)
			if (board.GetTile(whiteKingPoint).GetTileType() == TileType.Empty)
				board.SetTile(whiteKingPoint, new Tile(whiteKingPoint, TileType.White));
		foreach (Point2D blackKingPoint in blackKingPositions)
			if (board.GetTile(blackKingPoint).GetTileType() == TileType.Empty)
				board.SetTile(blackKingPoint, new Tile(blackKingPoint, TileType.Black));
	}
}