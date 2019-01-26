﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treehouse : Unit {

	[SerializeField]private Stage stage;

	private Shaker shaker;

	private void Start() {
		shaker	= GetComponent<Shaker>();
		shaker.enabled = false;
	}

	protected override void OnTakeDamage(int amount) {
		shaker.enabled = true;
		Vector3 curPos = transform.position;
		CustomCoroutine.WaitThenExecute(1.0f, () => {
				shaker.enabled = false;
				transform.position = curPos;
			}
		);
	}

	protected override void Die() {
		stage.GameOver();
	}
}
