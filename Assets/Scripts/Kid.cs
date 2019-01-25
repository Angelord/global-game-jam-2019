using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour {

	private static int lastIndex = 0;

	public float mvmSpeed;

	private int index;
	private Controls controls;
	private new Rigidbody2D rigidbody;

	private void Start() {
		index = lastIndex++;

		controls.Horizontal = "Horizontal_" + index;
		controls.Vertical = "Vertical_" + index;
		controls.Attack = "Throw_" + index;

		rigidbody = GetComponent<Rigidbody2D>();
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

		if(movDir.magnitude > 1.0f) {
			movDir.Normalize();
		}

		rigidbody.AddForce(movDir * mvmSpeed, ForceMode2D.Force);
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
