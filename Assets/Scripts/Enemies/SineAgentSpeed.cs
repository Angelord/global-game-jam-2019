using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SineAgentSpeed : MonoBehaviour {

	public float freq;
	public float minSpeed;

	private NavMeshAgent agent;
	private float maxSpeed;

	private void Start() {
		agent = GetComponent<NavMeshAgent>();
		maxSpeed = agent.speed;
	}

	private void Update() {

		float sine = Mathf.Sin((Time.time % freq) / freq);

		agent.speed = minSpeed + (maxSpeed - minSpeed) * sine;
	}
}
