using System;
using System.Collections.Generic;
using UnityEngine;


public class Game {
	private Board board;
	private BoardMover boardMover;
	private BoardExploder boardExploder;
	private BoardSpawner boardSpawner;
	private BoardLoader boardLoader;

	public Game(string rawBoard) {
		board = new Board(rawBoard);
		boardMover = new BoardMover(board);
		boardExploder = new BoardExploder(board);
		boardSpawner = new BoardSpawner(board);
		boardLoader = new BoardLoader(board);
	}


	public PlayState lastRealPlayState = PlayState.Continue;
	public PlayState SendCommand(int playerNumber, Direction direction) {

		boardSpawner.saveKingPositions();

		bool doesEffectBoard = boardMover.Move(playerNumber, direction);

		if (doesEffectBoard == false)
			return PlayState.Impossible;

		boardSpawner.trySpawningOnKingPositions();

		lastRealPlayState = boardExploder.ExplodeTrappedGroups(playerNumber.ToPlayerColor());
		return lastRealPlayState;

		//boardLoader.pushBoard(board, direction);

	}
	public Board GetBoard() {
		return board;
	}


	//a move is only available if it effects the board
	public List<Direction> findAvailableMoves(int playerNumber) {
		List<Direction> AvailableMoves = new List<Direction>();
		if (doesMoveEffectBoard(playerNumber, Direction.Up))
			AvailableMoves.Add(Direction.Up);
		if (doesMoveEffectBoard(playerNumber, Direction.Down))
			AvailableMoves.Add(Direction.Down);
		if (doesMoveEffectBoard(playerNumber, Direction.Left))
			AvailableMoves.Add(Direction.Left);
		if (doesMoveEffectBoard(playerNumber, Direction.Right))
			AvailableMoves.Add(Direction.Right);
		return AvailableMoves;
	}

	private bool doesMoveEffectBoard(int playerNumber, Direction direction) {

		pushMove(playerNumber, direction);
		bool doesEffectBoard = (boardLoader.Back().playState != PlayState.Impossible);
		popMove();

		return doesEffectBoard;
	}

	public string getStateAsString() {
		return board.getStateAsPrettyString();
	}

	public PlayState pushMove(int playerNumber, Direction direction) {
		//Debug.Log(board.getStateAsPrettyString());
		PlayState ps = SendCommand(playerNumber, direction);
		//Debug.Log(board.getStateAsPrettyString());
		boardLoader.PushBoardState(board, direction, ps );
		//Debug.Log(ps.ToString());
		return ps;
	}
	public void popMove() {
		boardLoader.PopBoardState();
		board.loadFromString(boardLoader.Back().board.getStateAsString());
		lastRealPlayState = boardLoader.Back().playState;
		//Debug.Log(lastRealPlayState.ToString());
	}
	public void clearMemory() {
		boardLoader.clearMemory();
	}
}
