using UnityEngine;
using System.Collections;

public class GameContainer : MonoBehaviour {
	private Game game;

	void Start () {
		game = new Game();
	}

	public Game GetGame() {
		return game;
	}
}
