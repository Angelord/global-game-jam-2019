using Events;
using UnityEngine;

public class CandyLoot : Loot {

	[SerializeField] private int amount = 1;

	protected override void OnPickUp() {
		EventManager.TriggerEvent(new CandyCollectedEvent());
		Progress.Candy += amount;
	}
}