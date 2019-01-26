using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public int health;

	public void TakeDamage(int amount) {
		health -= amount;
		if(health <= 0) {
			Die();
		}
	}

	protected abstract void Die();
}
