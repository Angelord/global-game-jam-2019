using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	[SerializeField] private int damage;
	[SerializeField] private float riseSpeed = 6.0f;
	[SerializeField] private float fallSpeed = 12.0f;
	[SerializeField] private float travelDist = 5.0f;
	[SerializeField] private GameObject explosion;
	[SerializeField] private float maxSize;
	[SerializeField] private float minSize;

	private float speed = 10.0f;
	private Vector3 startPos;
	private Vector3 movDir;
	private Direction direction;
	private GameObject sprite;

	private void Start() {
		sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
	}

	public void Launch(Direction direction) {
		this.speed = riseSpeed;
		this.direction = direction;
		this.startPos = transform.position;

		movDir = DirUtil.DirectionToVec(direction);
	}

	private void Update() {

		transform.Translate(movDir * speed * Time.deltaTime);

		float distanceTraveled = (transform.position - startPos).magnitude;

		if(distanceTraveled > travelDist / 2) {
			speed = fallSpeed;
		}

		if(distanceTraveled >= travelDist) {
			Explode();
		}

		float scale = minSize + (maxSize - minSize) * Mathf.Abs(distanceTraveled - travelDist) / (travelDist / 2);
		sprite.transform.localScale = new Vector3(scale, scale, scale);
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
