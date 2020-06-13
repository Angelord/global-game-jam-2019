﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Unit {

	[SerializeField] private float spawnsIn;
	[SerializeField] private GameObject creature;

	public override void Die() {
		
		CancelInvoke();
		
		EventManager.QueueEvent(new EnemyDiedEvent());
		
		Destroy(this.gameObject);
	}

	private void Start() {

		Invoke("Spawn", spawnsIn);
		EventManager.TriggerEvent(new EnemySpawnedEvent());
	}

	private void Spawn() {

		GameObject newEnemy = Instantiate(creature, transform.position, Quaternion.identity);
		Enemy enemyComp = newEnemy.GetComponent<Enemy>();
		if(enemyComp != null) {
			enemyComp.spawnedFromEgg = true;
		}

		Destroy(this.gameObject);
	}


	
}
