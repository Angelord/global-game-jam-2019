using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnorer : MonoBehaviour {

	[SerializeField] private string layerToIgnore;

	private void OnCollisionEnter(Collision collision) {
		Debug.Log("Collision enter");
		if (collision.gameObject.layer == LayerMask.NameToLayer(layerToIgnore)) {
			Debug.Log("Collision ignore");
			
			Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
		}
	}
}
