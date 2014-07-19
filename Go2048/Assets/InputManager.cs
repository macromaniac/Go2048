using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	Game game;

	void Start () {
		game = new Game(Util.startingMap);
	}
	
	void Update () {
	
	}
}
