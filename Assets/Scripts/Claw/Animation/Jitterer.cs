using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jitterer : MonoBehaviour {

	[SerializeField] private float shakeMagnitude = 0.7f;
 	[SerializeField] private float dampingSpeed = 1.0f;
	[SerializeField] private float shakeFreq = 0.05f;
 
	public void Jitter(float duration) {
		StartCoroutine(DoShake(duration));
	}

	private IEnumerator DoShake(float duration) {
		
		Vector3 initialPos = transform.localPosition;

		float timeRemaining = duration;

		do {

			transform.localPosition = initialPos + Random.insideUnitSphere * shakeMagnitude;

			timeRemaining -= shakeFreq * dampingSpeed;

			yield return shakeFreq;

		} while(timeRemaining > 0);

		transform.localPosition = initialPos;
	}
}
