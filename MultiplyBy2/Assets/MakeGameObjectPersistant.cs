using UnityEngine;
using System.Collections;
public class MakeGameObjectPersistant : MonoBehaviour {
	void Awake() {
		DontDestroyOnLoad(this);
		foreach (Transform child in transform)
			DontDestroyOnLoad(child.gameObject);
	}
}