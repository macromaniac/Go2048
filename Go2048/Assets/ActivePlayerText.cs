using UnityEngine;
using System.Collections;

public class ActivePlayerText : MonoBehaviour {
	public InputMan inputMan;
	TextMesh textMesh;

	void Start() {
		this.textMesh = gameObject.GetComponent<TextMesh>();
	}

	void Update() {
		string activePlayer = (inputMan.CurrentPlayer == 0) ? "White" : "Black";
		if (inputMan.gameOver) {
			textMesh.text = activePlayer + inputMan.gameOverState.ToString();
			return;
		}
		string activePlayerText = activePlayer + "'s Turn! ";
		textMesh.text = activePlayerText;
	}

}