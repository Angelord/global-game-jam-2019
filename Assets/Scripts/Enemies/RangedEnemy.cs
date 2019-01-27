using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy {
	
	public GameObject projectile;
	public float spawnDist;

	protected override void Attack() {

		LastAttack = Time.time;		
		Following = false;

		Vector3 targetDir = Target.transform.position - transform.position;
		Vector3 spawnPos = transform.position + targetDir.normalized * spawnDist;
		GameObject newProj = Instantiate(projectile, spawnPos, Quaternion.identity );
		newProj.GetComponent<EnemyProjectile>().Initialize(Target.transform.position, damage);

		Debug.Log("Attacking");

		CustomCoroutine.WaitThenExecute(attackCooldown, () => Following = true);
	}
}
