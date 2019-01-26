using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treehouse : Unit {

	protected override void OnTakeDamage(int amount) {
		Debug.Log("Taking damage");
	}

	protected override void Die() {
		Debug.Log("Treehouse dead");
	}
}
