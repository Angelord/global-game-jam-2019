using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	[SerializeField] private float shakeAmount = 1.0f;
	[SerializeField] private float shakeSpeed = 1.0f;

	private Vector3 initialPos;
	private bool shaking = false;

	public float ShakeAmount { get { return shakeAmount; } set { shakeAmount = value; } }
	public float ShakeSpeed { get { return shakeSpeed; } set { shakeSpeed = value; } }

	public void Shake(float duration) {
		StopAllCoroutines();

		if(shaking) {
			shaking = false;
			transform.localPosition = initialPos;
		}

		StartCoroutine(DoShake(duration));
	}

	private IEnumerator DoShake(float duration) {
		
		shaking = true;
		initialPos = transform.localPosition;
		
		float timeRemaining = duration;
		float shakeTime = 0.0f;
		
		do {

			Vector3 pos = transform.localPosition;
			pos.x = initialPos.x + Mathf.Sin(shakeTime) * shakeAmount;
			transform.localPosition = pos;

			shakeTime += Time.deltaTime * shakeSpeed;
			timeRemaining -= Time.deltaTime;
		
			yield return 0;

		} while(timeRemaining >= 0.0f || Vector3.Distance(initialPos, transform.localPosition) > 0.1f);

		shaking = false;
		transform.localPosition = initialPos;
	}
}
