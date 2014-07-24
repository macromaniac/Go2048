using UnityEngine;
using System.Collections;

public class ActivePlayerText : MonoBehaviour {
	TextMesh textMesh;

	private GameInterface gameInterface;
	void Start() {
		this.textMesh = gameObject.GetComponent<TextMesh>();
		this.gameInterface = GameInterfaceContainerScript.GetInterface();
	}

	void Update() {
		string activePlayer = (gameInterface.GetCurrentPlayer() == 0) ? "White" : "Black";
		if (gameInterface.IsGameOver()) {
			textMesh.text = activePlayer + gameInterface.GetPlayState().ToString();
			return;
		}
		string activePlayerText = activePlayer + "'s Turn! ";
		textMesh.text = activePlayerText;
	}

}