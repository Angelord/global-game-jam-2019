using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {
	
	[SerializeField] private GameObject projectile;
	[SerializeField] private float spawnDist;
	[SerializeField] private bool waitForAnim = true;
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

		if(waitForAnim) {
			yield return new WaitForSeconds(0.65f);
		}

		LaunchProjectile();

		CustomCoroutine.WaitThenExecute(attackCooldown, () => Following = true);
	}

	private void LaunchProjectile() {

		Vector3 targetDir = Target.transform.position - transform.position;
		Vector3 spawnPos = transform.position + targetDir.normalized * spawnDist;
		GameObject newProj = Instantiate(projectile, spawnPos, Quaternion.identity );
		newProj.GetComponent<EnemyProjectile>().Initialize(Target.transform.position, damage);

	}
}
