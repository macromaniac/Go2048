﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Board {
	private List<List<Tile>> board;
	public List<List<Tile>> GetBoard() { return board; }

	public Board(string rawBoard) {
		LoadFromString(rawBoard);
	}
	public void LoadFromBoard(Board boardToCopy){
		this.board = boardToCopy.GetBoard();
	}

	public void LoadFromString(string loadFrom){
		string[] mapLines = loadFrom.Split('\n');
		board = new List<List<Tile>>();

		for (int y = 0; y < mapLines.Length; ++y) {
			board.Add(new List<Tile>());
			List<Tile> lastRow = board[board.Count - 1];
			for (int x = 0; x < mapLines[y].Length; ++x)
				lastRow.Add(new Tile(new Point2D(x, y), mapLines[mapLines.Length - y - 1][x]));
		}
	}

	//List[x][y]
	public Tile GetKing(PlayerColor color) {
		foreach (List<Tile> row in board)
			foreach (Tile tile in row)
				if (tile.IsKing() && tile.GetColor() == color)
					return tile;
		return null;
	}
	public int NumHorizontalLines {
		get {
			return board.Count;
		}
	}
	public int NumVerticalLines {
		get {
			return board[0].Count;
		}
	}
	public int GetClosestPoint(Direction direction) {
		if (direction == Direction.Right)
			return GetFarthestPoint(Direction.Left);
		if (direction == Direction.Left)
			return GetFarthestPoint(Direction.Right);
		if (direction == Direction.Up)
			return GetFarthestPoint(Direction.Down);
		if (direction == Direction.Down)
			return GetFarthestPoint(Direction.Up);
		return 0;
	}
	public int GetFarthestPoint(Direction direction) {
		if (direction == Direction.Right)
			return board[0].Count - 1;
		if (direction == Direction.Left)
			return 0;
		if (direction == Direction.Up)
			return board.Count - 1;
		if (direction == Direction.Down)
			return 0;
		return 0;
	}

	public bool IsPointWithinBounds(Point2D point){
		if (point.y > NumHorizontalLines - 1 || point.y < 0)
			return false;
		if (point.x > NumVerticalLines - 1 || point.x < 0)
			return false;
		return true;
	}
	public Tile GetTile(Point2D point) {
		return board[point.y][point.x];
	}
	public Tile GetTile(int x, int y) {
		return GetTile(new Point2D(x, y));
	}
	public void SetTile(Point2D point, Tile toSet) {
		board[point.y][point.x] = toSet;
	}
	public void MoveToEmptyTile(Tile tile, Point2D totalMovement) {
		Tile originalTile = tile;
		Tile tileToMoveTo = GetTile(tile.pos + totalMovement);
		if (tileToMoveTo.GetTileType() != TileType.Empty)
			throw new InvalidOperationException();
		SetTile(tileToMoveTo.pos, new Tile(tileToMoveTo.pos, originalTile.GetTileType()));
		SetTile(originalTile.pos, new Tile(originalTile.pos, TileType.Empty));
	}
	public string GetStateAsPrettyString() {
		string stateAsString = "";
		for (int y = 0; y < board.Count; ++y) {
			for (int x = 0; x < board[y].Count; ++x) {
				string toAdd = GetTile(x, board.Count - y - 1).GetChar();
				if (toAdd == "x")
					toAdd = " ";
				stateAsString += toAdd + "\t";
			}
			stateAsString += "\n";
		}
		return stateAsString;
	}
	public string GetStateAsString() {
		string stateAsString = "";
		for (int y = 0; y < board.Count; ++y) {
			for (int x = 0; x < board[y].Count; ++x) {
				string toAdd = GetTile(x, board.Count - y - 1).GetChar();
				stateAsString += toAdd;
			}
			if (y != board.Count - 1)
				stateAsString += "\n";
		}
		return stateAsString;
	}

	public void ExplodeTile(Tile tile) {
		SetTile(tile.pos, new Tile(tile.pos, TileType.Empty));
	}

	public float NumTilesOfColor(PlayerColor color) {
		float counter=0;
		foreach (List<Tile> row in board)
			foreach (Tile tile in row)
				if (tile.GetColor() == color)
					counter += 1f;
		return counter;
	}
}
