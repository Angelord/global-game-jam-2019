using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryObject : MonoBehaviour {

	public float lifetime = 1.0f;

	private void Start() {
		Invoke("DestroySelf", lifetime);
	}

	private void DestroySelf() {
		Destroy(this.gameObject);
	}
}

