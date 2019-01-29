using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	[SerializeField] private float arcPeak = 0.6f;
	[SerializeField] private int damage;
	[SerializeField] private float minHeight = 0.0f;
	[SerializeField] private float maxHeight = 1.0f;
	[SerializeField] private float riseSpeed = 6.0f;
	[SerializeField] private float fallSpeed = 12.0f;
	[SerializeField] private GameObject explosion;
	[SerializeField] private float maxSize;
	[SerializeField] private float minSize;
	[SerializeField] private float horizontalTravelDist = 3.4f;
	[SerializeField] private float verticalTravelDist = 3.1f;

	private float speed = 10.0f;
	private float travelDist;
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

		if(direction == Direction.Left || direction == Direction.Right) {
			travelDist = horizontalTravelDist;
		}
		else {
			travelDist = verticalTravelDist;
		}

		movDir = DirUtil.DirectionToVec(direction);

		int soundToPlay = Random.Range(1, 5);
		AudioController.Instance.PlaySound("swish_" + soundToPlay);
	}

	private void Update() {

		transform.Translate(movDir * speed * Time.deltaTime);

		float distanceTraveled = (transform.position - startPos).magnitude;

		float peakDist = travelDist * arcPeak;

		float scale;
		if(distanceTraveled > peakDist) {
			speed = fallSpeed;
			float diff = distanceTraveled - peakDist;
			scale = maxSize - (diff / (travelDist - peakDist)) * (maxSize - minSize);
		}
		else {
			scale = minSize + (maxSize - minSize) * (distanceTraveled / peakDist);
		}
		sprite.transform.localScale = new Vector3(scale, scale, scale);

		if(direction == Direction.Right || direction == Direction.Left) {
			float height = 0.0f;
			if(distanceTraveled > peakDist) {
				float diff = distanceTraveled - peakDist;
				height = maxHeight - (diff / (travelDist - peakDist)) * (maxHeight - minHeight); 
			}
			else {
				height = minHeight + (maxHeight - minHeight) * (distanceTraveled / peakDist);
			}
			Vector3 spritePos = sprite.transform.localPosition;
			spritePos.z = height;
			sprite.transform.localPosition = spritePos;
		}

		if(distanceTraveled >= travelDist) {
			Explode();
		}
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			Explode();
		}
	}

	private void Explode() {
		GameObject newExp = Instantiate(explosion, transform.position, Quaternion.identity);
		newExp.GetComponent<Explosion>().Initialize(damage);
		Destroy(this.gameObject);
	}
}
