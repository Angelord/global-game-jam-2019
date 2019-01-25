using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health;
	public int damage;
	public float attackCooldown;


	public void TakeDamage(int amount) {
		health -= amount;
		if(health <= 0) {
			Die();
		}	
	} 

	private void Die() {
		Destroy(this.gameObject);
	}
}
