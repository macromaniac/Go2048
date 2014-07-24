using System;
using System.Collections.Generic;
using UnityEngine;


public class BoardState {
	public Board board;
	public Direction lastMoveDirection;
	public PlayState playState;
	public BoardState(Board board, Direction lastMoveDirection, PlayState playState) {
		this.board = board.DeepClone();

		//this.board = new Board(board.getStateAsString());

		this.lastMoveDirection = lastMoveDirection;
		this.playState = playState;
	}
}

public class BoardLoader {
	private List<BoardState> boardStates;

	public void PushBoardState(Board board, Direction direction, PlayState playState) {
		boardStates.Add(new BoardState(board, direction, playState));
	}
	public BoardState PopBoardState() {
		BoardState toReturn = boardStates[boardStates.Count - 1];

		boardStates.RemoveAt(boardStates.Count - 1);
		return toReturn;
	}

	public BoardState Back() {
		return boardStates[boardStates.Count - 1];
	}
	public BoardLoader(Board board) {
		boardStates = new List<BoardState>();
		PushBoardState(board, Direction.None, PlayState.Continue);
	}
	public void ClearMemory() {
		boardStates = new List<BoardState>();
	}
}
