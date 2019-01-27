﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Unit {

	[SerializeField] private GameObject barPrefab;
	[SerializeField] private float attackRate;
	[SerializeField] private int damage;
	[SerializeField] private float dealDmgDelay;

	private float lastAttack = 0.0f;
	private Animator animator;
	private List<Unit> targets = new List<Unit>();
	private Counterbar bar;
	private SpriteRenderer spriteRenderer;

	private void Start() {
		animator = GetComponentInChildren<Animator>();

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		bar = Instantiate(barPrefab, StageGUI.Instance.transform).GetComponent<Counterbar>();
		bar.SetValue(MaxHealth);
		bar.GetComponent<FollowerGUI>().SetTarget(transform);
	}

	public void AddTarget(Unit unit) {
		Debug.Log("Adding target");
		if(!targets.Contains(unit)) {
			targets.Add(unit);
		}
	}

	public void RemoveTarget(Unit unit) {
		if(targets.Contains(unit)) {
			targets.Remove(unit);
		}
	}

	protected override void OnTakeDamage(int amount) {
		int val = Mathf.Clamp(CurHealth - amount, 0, MaxHealth);
		bar.SetValue(val);
	}

	protected override void Die() {
		Destroy(this.gameObject);
		Destroy(bar);
	}

	private void Update() {
		for(int i = targets.Count - 1; i >= 0; i--) {
			if(targets[i].gameObject == null || targets[i].Dead) {
				targets.RemoveAt(i);
			}
		}

		if(Time.time - lastAttack > attackRate && targets.Count != 0) {
			
			StartCoroutine(Attack());

			lastAttack = Time.time;
		}
	}

	private IEnumerator Attack() {

		animator.SetTrigger("Attack");
		
		Unit target = targets[Random.Range(0, targets.Count)];

		spriteRenderer.flipX = target.transform.position.x < transform.position.x ? true : false;

		yield return new WaitForSeconds(dealDmgDelay);

		if(target.gameObject != null && !target.Dead) {
			target.TakeDamage(damage);
		}
	}

}
