using UnityEngine;
using System.Collections;
public class ChallengeButtonScript : Hittable {
	public override void OnHit() {
		Debug.Log("Challenge button Pressed");
	}
}