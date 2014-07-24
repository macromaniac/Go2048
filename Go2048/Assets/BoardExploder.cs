using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayState { Win, Loss, Draw, Continue, Impossible }
public class BoardExploder {
	private Board board;
	private List<List<bool>> checkedBoardTiles;

	private void ClearCheckedBoardTiles() {
		for (int i = 0; i < checkedBoardTiles.Count; ++i)
			for (int j = 0; j < checkedBoardTiles[0].Count; ++j)
				checkedBoardTiles[i][j] = false;

	}
	private bool CheckTile(Point2D point) {
		return checkedBoardTiles[point.y][point.x];
	}
	private void SetTile(Point2D point, bool val) {
		checkedBoardTiles[point.y][point.x] = val;
	}

	public BoardExploder(Board board) {
		this.board = board;
		checkedBoardTiles = new List<List<bool>>();
		for (int i = 0; i < board.NumHorizontalLines; ++i) {
			checkedBoardTiles.Add(new List<bool>());
			for (int j = 0; j < board.NumVerticalLines; ++j)
				checkedBoardTiles[checkedBoardTiles.Count - 1].Add(false);
		}
	}


	private void AddTileToList(ref bool isTouchingFreeSpace, ref List<Tile> list, Point2D point, PlayerColor color, Direction dir) {
		if (board.IsPointWithinBounds(point + dir.ToPoint2D())) {
			if (board.GetTile(point + dir.ToPoint2D()).GetColor() == color
			   && CheckTile(point + dir.ToPoint2D()) == false) {
				list.Add(board.GetTile(point + dir.ToPoint2D()));
				SetTile(point + dir.ToPoint2D(), true);
			}
			if (board.GetTile(point + dir.ToPoint2D()).GetTileType() == TileType.Empty)
				isTouchingFreeSpace = true;
		}

	}
	private void AddAdjacentTilesToList(ref bool isTouchingFreeSpace,
		ref List<Tile> list, Point2D point, PlayerColor color) {
		AddTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Down);
		AddTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Up);
		AddTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Left);
		AddTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Right);
	}

	private void CheckBoardTile(Point2D point) {
		//If it's already been checked then we're good to go
		if (CheckTile(point) == true)
			return;

		PlayerColor color = board.GetTile(point).GetColor();
		//If the tile has no color it's not going to explode, so boom, checked
		if (color == PlayerColor.None) {
			SetTile(point, true);
			return;
		}

		List<Tile> totalGroup = new List<Tile>();
		List<Tile> uncheckedTilesToJudge = new List<Tile>();

		bool oneIsTouchingFreeSpace = false;
		uncheckedTilesToJudge.Add(board.GetTile(point));
		SetTile(point, true);
		while (uncheckedTilesToJudge.Count > 0) {
			Tile toCheck = uncheckedTilesToJudge[uncheckedTilesToJudge.Count - 1];
			Point2D pos = toCheck.pos;
			AddAdjacentTilesToList(ref oneIsTouchingFreeSpace, ref uncheckedTilesToJudge,
				pos, color);

			uncheckedTilesToJudge.Remove(toCheck);
			totalGroup.Add(toCheck);
			SetTile(toCheck.pos, true);
		}

		//If the group of tiles isn't touching an empty tile it's trapped and it needs to go boom
		if (oneIsTouchingFreeSpace == false) {
			foreach (Tile tile in totalGroup) {
				if (tile.GetTileType() == TileType.BlackKing)
					blackKingKilled = true;
				if (tile.GetTileType() == TileType.WhiteKing)
					whiteKingKilled = true;
				board.ExplodeTile(tile);
			}
		}
	}
	bool whiteKingKilled = false;
	bool blackKingKilled = false;
	public PlayState ExplodeTrappedGroups(PlayerColor playerWhoMovedTheBoard) {
		whiteKingKilled = false;
		blackKingKilled = false;
		ClearCheckedBoardTiles();
		//A trapped group is group of blocks of a particular color that are not connected to an empty square
		for (int x = 0; x < board.NumVerticalLines; ++x) {
			for (int y = 0; y < board.NumHorizontalLines; ++y)
				CheckBoardTile(new Point2D(x, y));
		}

		if (whiteKingKilled && blackKingKilled)
			return PlayState.Draw;

		if (whiteKingKilled && playerWhoMovedTheBoard == PlayerColor.Black ||
				blackKingKilled && playerWhoMovedTheBoard == PlayerColor.White)
			return PlayState.Win;

		if (whiteKingKilled && playerWhoMovedTheBoard == PlayerColor.White ||
				blackKingKilled && playerWhoMovedTheBoard == PlayerColor.Black)
			return PlayState.Loss;

		return PlayState.Continue;
	}
}