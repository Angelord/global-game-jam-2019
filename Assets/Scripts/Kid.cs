using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour {

	private static int lastIndex = 0;

	public float mvmSpeed;
	public Transform directionArrow;

	private int index;
	private Controls controls;
	private new Rigidbody rigidbody;
	private Direction direction = Direction.Down;

	private void Start() {
		index = lastIndex++;

		controls.Horizontal = "Horizontal_" + index;
		controls.Vertical = "Vertical_" + index;
		controls.Attack = "Throw_" + index;

		rigidbody = GetComponent<Rigidbody>();
	}

	private void OnDestroy() {
		lastIndex--;
	}

	private void Update() {

		// if(Input.GetKeyDown(controls.Attack)) {
			// Attack();
		// }
	}

	private void FixedUpdate () {

		float hor = Input.GetAxis(controls.Horizontal);
		float ver = Input.GetAxis(controls.Vertical);
		
		Vector2 movDir = new Vector2(hor, ver);

		float magnitude = movDir.magnitude;
		if(magnitude > 0.0001f) {
			DetermineDir(hor, ver);
		}

		if(magnitude >= 1.0f) {
			movDir.Normalize();
		}

		Debug.Log("Adding force");
		rigidbody.AddForce(new Vector3(movDir.x, 0.0f, movDir.y) * mvmSpeed, ForceMode.Force);
	}

	private void DetermineDir(float hor, float ver) {

		if(Mathf.Abs(hor) > Mathf.Abs(ver)) {
			direction = hor > 0.0f ? Direction.Right : Direction.Left;
		}
		else {
			direction = ver > 0.0f ? Direction.Up : Direction.Down;
		}

		switch(direction) {
			case Direction.Down:
				directionArrow.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
				break;
			case Direction.Up:
				directionArrow.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
				break;
			case Direction.Left:
				directionArrow.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
				break;
			case Direction.Right:
				directionArrow.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
				break;
		}
	}

	private void Attack() {
		Debug.Log("Attacking");
	}

	private struct Controls {
		
		public string Horizontal { get; set; }
		public string Vertical { get; set; }
		public string Attack { get; set; }
	}
}
