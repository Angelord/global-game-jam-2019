using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Unit {

	[SerializeField] private float spawnsIn;
	[SerializeField] private GameObject creature;
	[SerializeField] private AudioClip hatchSound;

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
		
		if(hatchSound != null) {
			AudioController.Instance.PlayClipAt(hatchSound, transform.position);
		}

		Instantiate(creature, transform.position, Quaternion.identity);

		Destroy(this.gameObject);
	}


	
}
