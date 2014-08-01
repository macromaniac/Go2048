using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{ Draw, Lose, Win, InProgress };
public class BoardMover {
	Board board;
	public BoardMover(Board board) {
		this.board = board;
	}
	public GameState Move( Move move, PlayerNumber playerNumber) {
		return GameState.InProgress;
	}
}
