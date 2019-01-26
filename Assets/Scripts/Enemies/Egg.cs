using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Unit {

	[SerializeField] private float spawnsIn;
	[SerializeField] private GameObject creature;

	protected override void Die() {
		
		CancelInvoke();

		//TODO: play destroy anim
		Destroy(this.gameObject);
	}

	private void Start() {

		Invoke("Spawn", spawnsIn);
	}

	private void Spawn() {
		
		Instantiate(creature, transform.position, Quaternion.identity);
		//TODO : play destroy anim
		Destroy(this.gameObject);
	}


	
}
