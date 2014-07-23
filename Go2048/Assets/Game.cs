using System;
using System.Collections.Generic;


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


	public PlayState SendCommand(int playerNumber, Direction direction) {

		boardSpawner.saveKingPositions();

		bool doesEffectBoard = boardMover.Move(playerNumber, direction);

		if (doesEffectBoard == false)
			return PlayState.Impossible;

		boardSpawner.trySpawningOnKingPositions();

		return boardExploder.ExplodeTrappedGroups(playerNumber.ToPlayerColor());

		//boardLoader.pushBoard(board, direction);

	}

	public Direction GetAICommand(int playerNumber) {
		return Direction.None;
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

		boardLoader.pushBoard(board, Direction.None);
		bool doesEffectBoard = (SendCommand(playerNumber, direction) != PlayState.Impossible);
		board.loadFromBoard(boardLoader.popBoard());

		return doesEffectBoard;
	}

	public string getStateAsString() {
		return board.getStateAsPrettyString();
	}
}
