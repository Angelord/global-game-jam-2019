﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Unit {

	private const float DEATH_DELAY = 0.7f;

	private static Treehouse house;

	public int damage;
	public float attackRange;
	public float attackPushback;
	public float attackCooldown;

	private float lastAttack;
	private NavMeshAgent agent;
	private Unit target;

	public void SetTarget(Unit target) {
		this.target = target;
	}

	protected override void Die() {
		this.enabled = false;
		Invoke("DestroySelf", DEATH_DELAY);
	}

	private void DestroySelf() {
		Destroy(this.gameObject);
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
		if(!agent.enabled) { return; }

		if(!target.enabled) {
			target = house;
		}

		agent.SetDestination(target.transform.position);

		if(Vector3.Distance(target.transform.position, transform.position) <= attackRange) {
			Attack();
		}		
	}

	private void Attack() {
		if((Time.time - lastAttack) > attackCooldown) {
			target.TakeDamage(damage);
			Rigidbody enRigidbody = target.GetComponent<Rigidbody>();
			if(enRigidbody != null) {
				enRigidbody.AddForce((target.transform.position - transform.position).normalized * attackPushback, ForceMode.Impulse);
			}
			lastAttack = Time.time;
			agent.enabled = false;
			CustomCoroutine.WaitThenExecute(attackCooldown, () => agent.enabled = true);
		}
	}

	
}