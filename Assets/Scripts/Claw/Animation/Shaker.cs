using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	[SerializeField] private float shakeAmount = 1.0f;
	[SerializeField] private float shakeSpeed = 1.0f;
	
	public float ShakeAmount { get { return shakeAmount; } set { shakeAmount = value; } }
	public float ShakeSpeed { get { return shakeSpeed; } set { shakeSpeed = value; } }

	public void Shake(float duration) {
		StartCoroutine(DoShake(duration));
	}

	private IEnumerator DoShake(float duration) {
		Vector3 startPos = transform.localPosition;
		float shakeTime = 0.0f;
		do {

			Vector3 pos = transform.localPosition;
			pos.x = shakeAmount / 2 + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
			transform.localPosition = pos;

			shakeTime += Time.deltaTime;
		
			yield return 0;

		} while(shakeTime < duration || Vector3.Distance(startPos, transform.localPosition) < 0.0001f);

		transform.localPosition = startPos;
	}
}
