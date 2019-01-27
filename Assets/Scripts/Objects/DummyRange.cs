using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRange : MonoBehaviour {

	private Dummy dummy;

	private void Start() {
		dummy = GetComponentInParent<Dummy>();
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			dummy.AddTarget(other.GetComponent<Unit>());
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			dummy.RemoveTarget(other.GetComponent<Unit>());
		}
	}
}
