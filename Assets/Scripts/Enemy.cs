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
	private Unit target;

	public void SetTarget(Unit target) {
		this.target = target;
	}

	private void Start() {
		TryFindTreehouse();
		agent = GetComponent<NavMeshAgent>();
		target = house;
	}

	private void TryFindTreehouse() {
		if(house == null) {
			house = GameObject.FindGameObjectWithTag("Treehouse").GetComponent<Treehouse>();
		}
	}

	private void Update() {
		if(!target.enabled) {
			target = house;
		}

		agent.SetDestination(target.transform.position);
	}

	protected override void Die() {
		Destroy(this.gameObject);
	}
}
