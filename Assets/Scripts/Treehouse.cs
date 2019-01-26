using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treehouse : Unit {

	public Stage stage;

	protected override void OnTakeDamage(int amount) { }

	protected override void Die() {
		stage.GameOver();
	}
}
