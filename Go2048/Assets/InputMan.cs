using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputMan : MonoBehaviour {
	public TextMesh WorldText;
	public int playerNumber = 0;

	GameInterface gameInterface;
	void Start() {
		gameInterface = GameInterfaceContainerScript.GetInterface();
		WorldText.text = gameInterface.GetStateAsString();
	}


	void Update() {
		Direction directionFromInput = GetDirectionFromInput();
		if (directionFromInput != Direction.None) {
			gameInterface.TryToMove(playerNumber, directionFromInput);
			WorldText.text = gameInterface.GetStateAsString();
		}
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
