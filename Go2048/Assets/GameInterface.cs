using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInterface {
	Game game;
	PlayState currentPlayState;
	int currentPlayerNumber = 0;
	public GameInterface() {
		game = new Game(Util.startingMap);
		currentPlayState = PlayState.Continue;
	}

	public Board GetBoard() {
		return game.GetBoard();
	}

	public bool tryToMove(int playerNumber, Direction direction) {
		if (isGameOver() || playerNumber != currentPlayerNumber || !GetDirections(playerNumber).Contains(direction))
			return false;
		game.clearMemory();
		currentPlayState = game.pushMove(playerNumber, direction);
		SwitchActivePlayer();
		return true;
	}
	public bool isGameOver() {
		return (currentPlayState == PlayState.Draw || currentPlayState == PlayState.Win ||
				currentPlayState == PlayState.Loss);
	}

	public string GetStateAsString() {
		return game.getStateAsString();
	}
	public PlayState GetPlayState() {
		return currentPlayState;
	}
	private void SwitchActivePlayer() {
		if (currentPlayerNumber == 0)
			currentPlayerNumber = 1;
		else
			currentPlayerNumber = 0;
	}
	public int GetCurrentPlayer() {
		return currentPlayerNumber;
	}
	public List<Direction> GetDirections(int playerNumber) {
		return game.findAvailableMoves(playerNumber);
	}
}
