using UnityEngine;
using System.Collections;

public class PlayButtonScript : Hittable {
	void Update() {
	}

	public override void OnHit() {
		Debug.Log("Play button pressed");
	}
}