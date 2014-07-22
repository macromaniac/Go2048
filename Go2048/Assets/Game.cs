using System;
using System.Collections.Generic;


public class Game{
	private Board board;
	private BoardMover boardMover;
	private BoardExploder boardExploder;

	public Game(string rawBoard) {
		board = new Board(rawBoard);
		boardMover = new BoardMover(board);
		boardExploder = new BoardExploder(board);
	}

	public void SendCommand(int playerNumber, Direction direction) {
		List<Point2D> whiteKingPositions = new List<Point2D>();
		List<Point2D> blackKingPositions = new List<Point2D>();

		foreach (List<Tile> tileList in board.GetBoard())
			foreach (Tile tile in tileList)
				if (tile.GetTileType().isKing())
					if (tile.GetTileType().ToPlayerColor() == PlayerColor.White)
						whiteKingPositions.Add(tile.pos);
					else
						blackKingPositions.Add(tile.pos);

		boardMover.Move(playerNumber, direction);

		foreach (Point2D whiteKingPoint in whiteKingPositions)
			if (board.GetTile(whiteKingPoint).GetTileType() == TileType.Empty)
				board.SetTile(whiteKingPoint, new Tile(whiteKingPoint, TileType.White));

		foreach (Point2D blackKingPoint in blackKingPositions)
			if (board.GetTile(blackKingPoint).GetTileType() == TileType.Empty)
				board.SetTile(blackKingPoint, new Tile(blackKingPoint, TileType.Black));

		boardExploder.ExplodeTrappedGroups(playerNumber.ToPlayerColor());
	}
	public string getStateAsString() {
		return board.getStateAsString();
	}
}
