using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {


	public float speed = 10.0f;
	public float travelDist = 5.0f;
	// public float falloffDist = 3.0f;

	private Vector3 startPos;
	private Vector3 movDir;
	private Direction direction;

	public void Launch(Direction direction) {
		this.direction = direction;
		this.startPos = transform.position;

		movDir = DirUtil.DirectionToVec(direction);
		
	}

	private void Update() {
		transform.Translate(movDir * speed * Time.deltaTime);

		if((transform.position - startPos).magnitude >= travelDist) {
			Debug.Log("Distance travelled");
			Explode();
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			Explode();
		}
	}

	private void Explode() {
		Debug.Log("Exploding");
		Destroy(this.gameObject);
	}
}
