using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	public int damage;
	public float speed = 10.0f;
	public float travelDist = 5.0f;
	public GameObject explosion;
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
			Explode();
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			Debug.Log("Hit enemy");
			Explode();
		}
	}

	private void Explode() {
		GameObject newExp = Instantiate(explosion, transform.position, Quaternion.identity);
		newExp.GetComponent<Explosion>().Initialize(damage);
		Destroy(this.gameObject);
	}
}
