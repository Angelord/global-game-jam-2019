using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : MonoBehaviour {

	[SerializeField]private float followSpeed = 5.0f;
	[SerializeField]private float pickUpRange = 0.4f;

	private Transform targetPlayer;

	private void OnTriggerEnter(Collider other) {
		if(targetPlayer == null && other.gameObject.tag == "Player") {

			targetPlayer = other.gameObject.transform;
		}
	}

	private void Update() {
		if(targetPlayer == null) { return; }

		float step = Time.deltaTime * followSpeed;
		transform.position	= Vector3.MoveTowards(transform.position, targetPlayer.position, step);
	
		if(Vector3.Distance(transform.position, targetPlayer.position) < pickUpRange) {
			OnPickUp();
			Destroy(this.gameObject);
		}
	}

	protected abstract void OnPickUp();
}
