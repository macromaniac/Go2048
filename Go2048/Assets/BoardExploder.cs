using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WinState { Win, Loss, Draw, Continue }
public class BoardExploder {
	private Board board;
	private List<List<bool>> checkedBoardTiles;

	private void clearCheckedBoardTiles() {
		for (int i = 0; i < checkedBoardTiles.Count; ++i)
			for (int j = 0; j < checkedBoardTiles[0].Count; ++j)
				checkedBoardTiles[i][j] = false;

	}
	private bool checkTile(Point2D point) {
		return checkedBoardTiles[point.y][point.x];
	}
	private void setTile(Point2D point, bool val) {
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


	private void addTileToList(ref bool isTouchingFreeSpace, ref List<Tile> list, Point2D point, PlayerColor color, Direction dir) {
		if (board.isPointWithinBounds(point + dir.ToPoint2D())) {
			if (board.GetTile(point + dir.ToPoint2D()).GetColor() == color
			   && checkTile(point + dir.ToPoint2D()) == false) {
				list.Add(board.GetTile(point + dir.ToPoint2D()));
				setTile(point + dir.ToPoint2D(), true);
			}
			if (board.GetTile(point + dir.ToPoint2D()).GetTileType() == TileType.Empty)
				isTouchingFreeSpace = true;
		}

	}
	private void addAdjacentTilesToList(ref bool isTouchingFreeSpace,
		ref List<Tile> list, Point2D point, PlayerColor color) {
		addTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Down);
		addTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Up);
		addTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Left);
		addTileToList(ref isTouchingFreeSpace, ref list, point, color, Direction.Right);
	}

	private void checkBoardTile(Point2D point) {
		//If it's already been checked then we're good to go
		if (checkTile(point) == true)
			return;

		PlayerColor color = board.GetTile(point).GetColor();
		//If the tile has no color it's not going to explode, so boom, checked
		if (color == PlayerColor.None) {
			setTile(point, true);
			return;
		}

		//get the group of tiles that belong to it, and while we're getting that group, 
		//see if any of the tiles are adjacent to this group are empty
		List<Tile> totalGroup = new List<Tile>();
		List<Tile> uncheckedTilesToJudge = new List<Tile>();
		bool oneIsTouchingFreeSpace = false;
		//Add tiles to unchecked tiles to judge, then consider them checked and pop them off the list
		//after adding all viable neighbors, do the same. if any are touching a free space set that bool to true
		//then afterwards do not destroy them unless none are touching a free space
		uncheckedTilesToJudge.Add(board.GetTile(point));
		setTile(point, true);
		while (uncheckedTilesToJudge.Count > 0) {
			Tile toCheck = uncheckedTilesToJudge[uncheckedTilesToJudge.Count - 1];
			Point2D pos = toCheck.pos;
			addAdjacentTilesToList(ref oneIsTouchingFreeSpace, ref uncheckedTilesToJudge,
				pos, color);

			//Add to total group
			uncheckedTilesToJudge.Remove(toCheck);
			totalGroup.Add(toCheck);
			setTile(toCheck.pos, true);
		}

		if (oneIsTouchingFreeSpace == false) {
			//string fullString = "";
			//foreach (Tile tile in totalGroup)
			//	fullString += tile.pos.ToString() + " " + tile.GetChar() + " ";
			//Debug.Log(fullString);

			foreach (Tile tile in totalGroup) {
				board.explodeTile(tile);
			}
		}
	}
	public WinState ExplodeTrappedGroups(PlayerColor playerWhoMovedTheBoard) {
		clearCheckedBoardTiles();
		//A trapped group is group of blocks of a particular color that are not connected to an empty square
		for (int x = 0; x < board.NumVerticalLines; ++x) {
			for (int y = 0; y < board.NumHorizontalLines; ++y)
				checkBoardTile(new Point2D(x, y));

		}

		return WinState.Continue;
	}
}