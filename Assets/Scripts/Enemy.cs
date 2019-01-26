using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	private static Treehouse house;

	public int health;
	public int damage;
	public float attackCooldown;
	private NavMeshAgent agent;

	private void TryFindTreehouse() {
		if(house == null) {
			house = GameObject.FindGameObjectWithTag("Treehouse").GetComponent<Treehouse>();
		}
	}

	private void Start() {
		TryFindTreehouse();
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(house.transform.position);
	}

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
