using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour {

	[SerializeField] private GameObject egg;
	[SerializeField] private float spawnFreq;
	[SerializeField] private float spawnDuration;

	private float lastSpawn;
	private Animator animator;
	private NavMeshAgent agent;

	private void Start() {
		animator = GetComponentInChildren<Animator>();
		agent = GetComponentInChildren<NavMeshAgent>();

		lastSpawn = Time.time + spawnFreq;
	}

	private void Update() {
		if(Stage.Playing && Time.time - lastSpawn > spawnFreq) {
			StartCoroutine(SpawnEgg());
			lastSpawn = Time.time;
		}	
	}

	private IEnumerator SpawnEgg() {
		
		animator.SetTrigger("Lay");
		agent.enabled = false;

		yield return new WaitForSeconds(spawnDuration);

		Instantiate(egg, transform.position, Quaternion.identity);
		agent.enabled = true;
	}
}
