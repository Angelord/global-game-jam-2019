using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour {

	private Enemy enemy;

	private void Start() {
		enemy = transform.parent.GetComponent<Enemy>();
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			enemy.SetTarget(other.gameObject.GetComponent<Kid>());
		}
	}
}

