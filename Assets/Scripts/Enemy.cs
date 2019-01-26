using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Unit {

	private static Treehouse house;

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

	protected override void Die() {
		Destroy(this.gameObject);
	}
}
