using System;
using System.Collections.Generic;
using UnityEngine;


public class BoardState {
	public Board board;
	public Direction lastMoveDirection;
	public BoardState(Board board, Direction lastMoveDirection) {
		this.board = Util.DeepClone<Board>(board);
		this.lastMoveDirection = lastMoveDirection;
	}
}

public class BoardLoader {
	private List<BoardState> boardStates;

	public void pushBoard(Board board, Direction direction) {
		boardStates.Add(new BoardState(board, direction));
	}
	public Board popBoard() {
		Board toReturn = boardStates[boardStates.Count - 1].board;

		boardStates.RemoveAt(boardStates.Count - 1);
		return toReturn;
	}
	public BoardLoader(Board board) {
		boardStates = new List<BoardState>();
		pushBoard(board, Direction.None);
	}
}
