using System;
using System.Collections.Generic;


public class Game{
	private Board board;
	public Game(string rawBoard) {
		board = new Board(rawBoard);
		board.printBoardToConsole();
	}
	public bool SendCommand(int playerNumber, Direction command) {
		return false;
	}
}
