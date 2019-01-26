using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	[SerializeField] private float shakeAmount = 1.0f;
	[SerializeField] private float shakeSpeed = 1.0f;

	private bool shaking = true;
	private Vector3 startPos;
	private Vector3 targetPos;

	public void Shake() {

		startPos = transform.position;

		targetPos = startPos;
		targetPos.x -= shakeAmount;

		shaking = true;
	}
	
	private void Update() {
		if(!shaking) { return; }

		Vector3 shakeDir = targetPos - transform.position;

			transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * shakeSpeed);
			// shakeDir.Normalize();
			// transform.Translate(shakeDir * shakeSpeed * Time.deltaTime);

		if(shakeDir.magnitude <= 0.0001f) {
			if(Mathf.Abs(targetPos.x - startPos.x) <= 0.00001f) {
				transform.position = startPos;
				shaking = false;
			}
			else {
				targetPos = startPos;
				targetPos.x += shakeAmount;
			}
		}
	}
}
