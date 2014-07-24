using UnityEngine;
using System.Collections;

public class AIScript : MonoBehaviour {

	public TextMesh WorldText;
	public int playerNumber = 1;
	float AITurnTime = 1f;

	GameInterface gameInterface;
	void Start() {
		gameInterface = GameInterfaceContainerScript.GetInterface();
	}

	bool isMakingMove = false;
	void Update() {
		if (gameInterface.GetCurrentPlayer() == playerNumber && isMakingMove == false) {
			isMakingMove = true;
			MakeMove();
		}
	}

	void MakeMove() {
		StartCoroutine(FindBestMove());
	}

	IEnumerator FindBestMove() {
		Debug.Log("waiting");
		yield return new WaitForSeconds(AITurnTime);
		AI ai = new AI(gameInterface.GetBoard(),gameInterface.GetPlayState(), playerNumber.ToPlayerColor());
		Direction dirToMove = ai.FindBestMove();
		gameInterface.TryToMove(playerNumber, dirToMove);
		Debug.Log("AI Says: Done making move. " + dirToMove.ToString());
		WorldText.text = gameInterface.GetStateAsString();
		isMakingMove = false;

	}

}
