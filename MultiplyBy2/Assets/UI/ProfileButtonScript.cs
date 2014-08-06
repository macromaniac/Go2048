using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProfileButtonScript : Hittable {
	public override void OnHit() {
		Debug.Log("Profile button pressed");
	}
}