using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public int health;

	public void TakeDamage(int amount) {
		OnTakeDamage(amount);
		health -= amount;
		if(health <= 0) {
			Die();
		}
	}

	protected virtual void OnTakeDamage(int amount) { }

	protected abstract void Die();
}
