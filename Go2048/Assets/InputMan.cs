using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputMan : MonoBehaviour {
	public TextMesh WorldText;
	Game game;

	void Start() {
		game = new Game(Util.startingMap);
		WorldText.text = game.getStateAsString();
	}

	//Players index at 0
	int currentPlayer = 0;
	public int CurrentPlayer { get { return currentPlayer; } }

	void Update() {
		Direction directionFromInput = GetDirectionFromInput();
		if (directionFromInput != Direction.None) {
			AcceptInput(directionFromInput);
		}
	}

	private void AcceptInput(Direction direction) {
		game.SendCommand(currentPlayer, direction);
		switchPlayer();
		WorldText.text = game.getStateAsString();
	}

	private void switchPlayer() {
		if (currentPlayer == 0)
			currentPlayer = 1;
		else
			currentPlayer = 0;
	}
	private Direction GetDirectionFromInput() {
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			return Direction.Up;
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			return Direction.Down;
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			return Direction.Left;
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			return Direction.Right;
		return Direction.None;
	}
}
