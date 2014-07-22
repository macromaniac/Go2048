using UnityEngine;
using System.Collections;

public class ActivePlayerText : MonoBehaviour {
	public InputMan inputMan;
	TextMesh textMesh;

	void Start() {
		this.textMesh = gameObject.GetComponent<TextMesh>();
	}

	void Update() {
		string activePlayerText = ((inputMan.CurrentPlayer == 0) ? "White's " : "Blacks's ") + "Turn! ";
		textMesh.text = activePlayerText;
	}

}
