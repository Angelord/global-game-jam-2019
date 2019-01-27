using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

	public float speed;
	public float lifetime;

	private Vector3 mvDir;
	private int damage;
	private float launchTime;

	public void Initialize(Vector3 target, int damage) {
		target.y = 0;
		mvDir = target - transform.position;
		mvDir.y = 0.0f;
		this.damage = damage;
		mvDir.Normalize();

		transform.GetChild(0).rotation = Quaternion.FromToRotation(-1 * Vector3.forward, mvDir);

		this.launchTime = Time.time;
	}

	private void Update() {
		if(Time.time - launchTime >= lifetime) {
			Destroy(this.gameObject);
			return;
		}

		transform.Translate(mvDir * speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Treehouse") {
			other.gameObject.GetComponent<Unit>().TakeDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
