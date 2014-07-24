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

	public bool TryToMove(int playerNumber, Direction direction) {
		if (IsGameOver() || playerNumber != currentPlayerNumber || !GetDirections(playerNumber).Contains(direction))
			return false;
		game.ClearMemory();
		currentPlayState = game.PushMove(playerNumber, direction);
		SwitchActivePlayer();
		return true;
	}
	public bool IsGameOver() {
		return (currentPlayState == PlayState.Draw || currentPlayState == PlayState.Win ||
				currentPlayState == PlayState.Loss);
	}

	public string GetStateAsString() {
		return game.GetStateAsString();
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
		return game.FindAvailableMoves(playerNumber);
	}
}
