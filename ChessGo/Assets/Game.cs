using System;
using System.Collections.Generic;

using UnityEngine;

public enum PlayerNumber { One, Two };

public class Game {
	private PlayerNumber currentPlayerNumber = PlayerNumber.One;
	private Board board;
	public Game() {
		board = new Board(Util.rawMap);
		Debug.Log(board.ToPrettyString());
	}
	public List<Move> GetAvailableMoves() {
		List<Move> toReturn = new List<Move>();
		return toReturn;
	}

	public PlayerNumber CurrentPlayerNumber {
		get {
			return currentPlayerNumber;
		}
	}
}
