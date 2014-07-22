using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMover {
	Board board;
	public BoardMover(Board board) {
		this.board = board;
	}

	public Board Move(int playerNumber, Direction direction) {
		//Tile kingTile = board.GetKing(playerNumber.ToPlayerColor());

		//The method for movement starts at the lowest one
		
		//Get farthest empty Tile in that Direction on each line, then move up until you find a 
		//movable Tile. Move that tile as far down as possible, then repeat this entire thing, but reduce
		//the range of the search up until where you moved the tile. If you had moved a king,
		//reduce the range of the search up to where the king previously was

		MoveDirection(playerNumber, direction);
		return board;
	}

	private void MoveDirection(int playerNumber, Direction direction) {


		int min = board.GetClosestPoint(direction);
		int max = board.GetFarthestPoint(direction);

		//Need to find min / max based off of the king that is most inclusive
		Tile king = board.GetKing(playerNumber.ToPlayerColor());
		if(direction == Direction.Right || direction==Direction.Left){

			if (direction == Direction.Right)
				min = king.pos.x - 1;
			if (direction == Direction.Left)
				min = king.pos.x + 1;


			for (int i = 0; i < board.NumHorizontalLines; ++i) {
				Point2D origin = new Point2D(min, i);
				Point2D end = new Point2D(max, i);
				Point2D step = direction.ToPoint2D();
				MoveLine(origin, end, step);
			}
		}

		if(direction == Direction.Up || direction==Direction.Down){


			if (direction == Direction.Up)
				min = king.pos.y - 1;
			if (direction == Direction.Down)
				min = king.pos.y + 1;

			for (int i = 0; i < board.NumVerticalLines; ++i) {
				Point2D origin = new Point2D(i, min);
				Point2D end = new Point2D(i, max);
				Point2D step = direction.ToPoint2D();
				MoveLine(origin, end, step);
			}
		}

	}

	private void MoveLine(Point2D origin, Point2D end, Point2D step, Point2D wall = null) {
		//The recursion actually starts at end and lowers the tail until it is origin by subtracting steps.
		//This is to slide objects down without other objects getting in the way

		if (origin.isEqualTo(end)) //Recursion end condition when origin == end
			return;

		Tile endTile = board.GetTile(end);

		if (endTile.GetTileType().isMovableType() == false) {
			MoveLine(origin, end - step, step, wall);
			return;
		}

		//move the tile down as far as possible
		int howFarCanIMove = 0;
		while (board.GetTile(end + step * (howFarCanIMove+1)).GetTileType() == TileType.Empty &&
			(wall == null || !wall.isEqualTo(end + step * (howFarCanIMove + 1))))
			howFarCanIMove++;

		if(howFarCanIMove!=0)
			board.MoveToEmptyTile(board.GetTile(end), howFarCanIMove * step);

		if (endTile.GetTileType().isKing()) {
			MoveLine(origin, end - step, step, endTile.pos);
		}
		else {
			MoveLine(origin, end - step, step, wall);
		}
	}

}