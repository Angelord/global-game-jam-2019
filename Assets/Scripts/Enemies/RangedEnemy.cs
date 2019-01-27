using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {
	
	public GameObject projectile;
	public float spawnDist;
	private Animator animator;

	protected override void Attack() {
		if(animator == null) {
			animator = GetComponentInChildren<Animator>();
		}

		StartCoroutine(AttackCoroutine());
		
	}

	private IEnumerator AttackCoroutine() {

		LastAttack = Time.time;		
		Following = false;

		animator.SetTrigger("Attack");

		yield return new WaitForSeconds(0.65f);

		Vector3 targetDir = Target.transform.position - transform.position;
		Vector3 spawnPos = transform.position + targetDir.normalized * spawnDist;
		GameObject newProj = Instantiate(projectile, spawnPos, Quaternion.identity );
		newProj.GetComponent<EnemyProjectile>().Initialize(Target.transform.position, damage);

		CustomCoroutine.WaitThenExecute(attackCooldown, () => Following = true);
	}
}
