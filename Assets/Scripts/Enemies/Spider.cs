using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour {

	[SerializeField] private GameObject egg;
	[SerializeField] private float spawnFreq = 1.1f;
	[SerializeField] private float spawnDuration = 1.1f;
	[SerializeField] private float startSpawnDelay = 3.0f;
	public AK.Wwise.Event EggSpawnedAudioEv;
	
	private bool spawning;
	private float lastSpawn;
	private Animator animator;
	private NavMeshAgent agent;

	private void Start() {
		animator = GetComponentInChildren<Animator>();
		agent = GetComponentInChildren<NavMeshAgent>();

		lastSpawn = Time.time + startSpawnDelay;
	}

	private void Update() {
		if(Stage.Playing && !spawning && Time.time - lastSpawn > spawnFreq) {
			StartCoroutine(SpawnEgg());
		}	
	}

	private IEnumerator SpawnEgg() {
		spawning = true;

		animator.SetTrigger("Lay");
		agent.enabled = false;

		yield return new WaitForSeconds(spawnDuration);

		EggSpawnedAudioEv.Post(gameObject);

		Instantiate(egg, transform.position, Quaternion.identity);
		agent.enabled = true;

		spawning = false;
		lastSpawn = Time.time;
	}
}
