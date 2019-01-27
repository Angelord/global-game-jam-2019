using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Unit {

	[SerializeField] private float spawnsIn;
	[SerializeField] private GameObject creature;

	protected override void Die() {
		
		CancelInvoke();

		EventManager.QueueEvent(new EnemyDiedEvent());
		
		Destroy(this.gameObject);
	}

	private void Start() {

		Invoke("Spawn", spawnsIn);
		EventManager.TriggerEvent(new EnemySpawnedEvent());
	}

	private void Spawn() {
		
		Instantiate(creature, transform.position, Quaternion.identity);

		EventManager.QueueEvent(new EnemyDiedEvent());

		Destroy(this.gameObject);
	}


	
}
