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

		boardSpawner.SaveKingPositions();

		bool doesEffectBoard = boardMover.Move(playerNumber, direction);

		if (doesEffectBoard == false)
			return PlayState.Impossible;

		boardSpawner.TrySpawningOnKingPositions();

		lastRealPlayState = boardExploder.ExplodeTrappedGroups(playerNumber.ToPlayerColor());
		return lastRealPlayState;

		//boardLoader.pushBoard(board, direction);

	}
	public Board GetBoard() {
		return board;
	}


	//a move is only available if it effects the board
	public List<Direction> FindAvailableMoves(int playerNumber) {
		List<Direction> AvailableMoves = new List<Direction>();
		if (DoesMoveEffectBoard(playerNumber, Direction.Up))
			AvailableMoves.Add(Direction.Up);
		if (DoesMoveEffectBoard(playerNumber, Direction.Down))
			AvailableMoves.Add(Direction.Down);
		if (DoesMoveEffectBoard(playerNumber, Direction.Left))
			AvailableMoves.Add(Direction.Left);
		if (DoesMoveEffectBoard(playerNumber, Direction.Right))
			AvailableMoves.Add(Direction.Right);
		return AvailableMoves;
	}

	private bool DoesMoveEffectBoard(int playerNumber, Direction direction) {

		PushMove(playerNumber, direction);
		bool doesEffectBoard = (boardLoader.Back().playState != PlayState.Impossible);
		PopMove();

		return doesEffectBoard;
	}

	public string GetStateAsString() {
		return board.GetStateAsPrettyString();
	}

	public PlayState PushMove(int playerNumber, Direction direction) {
		//Debug.Log(board.getStateAsPrettyString());
		PlayState ps = SendCommand(playerNumber, direction);
		//Debug.Log(board.getStateAsPrettyString());
		boardLoader.PushBoardState(board, direction, ps );
		//Debug.Log(ps.ToString());
		return ps;
	}
	public void PopMove() {
		boardLoader.PopBoardState();
		board.LoadFromString(boardLoader.Back().board.GetStateAsString());
		lastRealPlayState = boardLoader.Back().playState;
		//Debug.Log(lastRealPlayState.ToString());
	}
	public void ClearMemory() {
		boardLoader.ClearMemory();
	}
}
