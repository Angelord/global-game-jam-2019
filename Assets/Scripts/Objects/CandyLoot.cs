using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyLoot : Loot {

	[SerializeField] private int amount = 1;

	protected override void OnPickUp() {
		Progress.Candy += amount;
	}
}