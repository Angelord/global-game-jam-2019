using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyLoot : Loot {

	protected override void OnPickUp() {
		Debug.Log("Picked up");
	}
}
