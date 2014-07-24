using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMover {
	Board board;
	public BoardMover(Board board) {
		this.board = board;
	}

	public bool Move(int playerNumber, Direction direction) {

		return MoveDirection(playerNumber, direction);
	}

	bool didMove = false;
	private bool MoveDirection(int playerNumber, Direction direction) {
		didMove = false;

		int startMove = board.GetClosestPoint(direction);
		int endMove = board.GetFarthestPoint(direction);

		//Need to find start based off of the king that is most inclusive
		Tile king = board.GetKing(playerNumber.ToPlayerColor());

		if (king == null)
			Debug.Log(playerNumber + " " +board.GetStateAsString());

		if (direction == Direction.Right || direction == Direction.Left) {

			if (direction == Direction.Right)
				startMove = king.pos.x - 1;
			if (direction == Direction.Left)
				startMove = king.pos.x + 1;


			for (int i = 0; i < board.NumHorizontalLines; ++i) {
				Point2D origin = new Point2D(startMove, i);
				Point2D end = new Point2D(endMove, i);
				Point2D step = direction.ToPoint2D();
				MoveLine(origin, end, step);
			}
		}

		if (direction == Direction.Up || direction == Direction.Down) {


			if (direction == Direction.Up)
				startMove = king.pos.y - 1;
			if (direction == Direction.Down)
				startMove = king.pos.y + 1;

			for (int i = 0; i < board.NumVerticalLines; ++i) {
				Point2D origin = new Point2D(i, startMove);
				Point2D end = new Point2D(i, endMove);
				Point2D step = direction.ToPoint2D();
				MoveLine(origin, end, step);
			}
		}
		return didMove;
	}

	private void MoveLine(Point2D origin, Point2D end, Point2D step, Point2D wall = null) {
		//The recursion actually starts at end and lowers the tail until it is origin by subtracting steps.
		//This is to slide objects down without other objects getting in the way

		if (origin.IsEqualTo(end)) //Recursion end condition when origin == end
			return;

		Tile endTile = board.GetTile(end);

		if (endTile.GetTileType().IsMovableType() == false) {
			MoveLine(origin, end - step, step, wall);
			return;
		}

		//move the tile down as far as possible
		int howFarCanIMove = 0;
		while (board.GetTile(end + step * (howFarCanIMove + 1)).GetTileType() == TileType.Empty &&
			(wall == null || !wall.IsEqualTo(end + step * (howFarCanIMove + 1))))
			howFarCanIMove++;

		if (howFarCanIMove != 0) {
			board.MoveToEmptyTile(board.GetTile(end), howFarCanIMove * step);
			didMove = true;
		}

		if (endTile.GetTileType().IsKing()) {
			MoveLine(origin, end - step, step, endTile.pos);
		}
		else {
			MoveLine(origin, end - step, step, wall);
		}
	}

}