using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float explosionForce = 1.0f;
	private int damage;
	private SphereCollider sphereCollider;

	private void Start() {
		sphereCollider = GetComponent<SphereCollider>();
	}

	public void Initialize(int damage) {
		this.damage = damage;
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			other.GetComponent<Enemy>().TakeDamage(damage);
			other.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, sphereCollider.radius, 0.0f, ForceMode.Impulse);
		}
	} 
}
