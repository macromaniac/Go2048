using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hittable : MonoBehaviour {
	public bool actOnRelease;

	bool isMouseOverHitBox(Vector3 inputPos) {
		inputPos = Camera.main.ScreenToWorldPoint(inputPos);
		inputPos = new Vector3(inputPos.x, inputPos.y, renderer.bounds.center.z);
		if (renderer.bounds.Contains(inputPos))
			return true;
		return false;
	}

	void LateUpdate() {
		bool isInputUp = false;
		bool isInputDown = false;
		Vector3 inputPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		if (Input.mousePresent) {
			isInputUp = Input.GetMouseButtonUp(0);
			isInputDown = Input.GetMouseButtonDown(0);
			inputPos = Input.mousePosition;
		}
		else {
			if (Input.touchCount > 0) {
				if (Input.touches[0].phase == TouchPhase.Ended)
					isInputUp = true;
				if (Input.touches[0].phase == TouchPhase.Began)
					isInputDown = true;
				inputPos = Input.touches[0].position;
			}
		}

		if (actOnRelease) {
			if (isInputUp == true && isMouseOverHitBox(inputPos) == true) 
				OnHit();
		}
		else {
			if (isInputDown == true && isMouseOverHitBox(inputPos) == true)
				OnHit();
		}
	}
	public virtual void OnHit() {
	}
}