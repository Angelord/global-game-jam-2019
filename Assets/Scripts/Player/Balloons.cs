using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : Usable {

	[SerializeField] private float cooldown;
	[SerializeField] private GameObject balloon;
	[SerializeField] private Transform spawnPoint;

	private float lastAttack;

	private void Start() {
		lastAttack = -cooldown;
	}
	
	public override void Use() {
		if((Time.time - lastAttack) >= cooldown) {
			GameObject toLaunch = Instantiate(balloon, spawnPoint.position, Quaternion.identity);
			toLaunch.GetComponent<Balloon>().Launch(Kid.Direction);
			lastAttack = Time.time;
		}
	}
}
