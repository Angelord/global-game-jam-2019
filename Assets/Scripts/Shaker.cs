using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	[SerializeField] private float shakeAmount = 1.0f;
	[SerializeField] private float shakeSpeed = 1.0f;
	
	private void Update() {

		Vector3 pos = transform.position;

		pos.x = shakeAmount / 2 + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

		transform.position = pos;
	}
}
