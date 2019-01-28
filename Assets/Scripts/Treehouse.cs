using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treehouse : Unit {

	[SerializeField] private Stage stage;
	[SerializeField] private int[] levelHealths;
	[SerializeField] private float[] levelRegens;
	[SerializeField] private Collider[] levelColliders;
	[SerializeField] private ParticleSystem regenEffect;

	private Shaker shaker;
	private float regen;
	private float lastRegen;

	private void Start() {

		shaker	= GetComponentInChildren<Shaker>();
		shaker.enabled = false;

		GetComponentInChildren<Animator>().SetInteger("Level", Progress.HouseLevel);
	
		EventManager.AddListener<StageStartedEvent>(HandleStageStartedEvent);
	}

	private void HandleStageStartedEvent(StageStartedEvent stageEv) {
		int level = Progress.HouseLevel;

		for(int i = 0; i < levelColliders.Length; i++) {
			levelColliders[i].enabled = (i == level);
		}

		SetMaxHealth(levelHealths[level]);
		ResetHealth();
		regen = levelRegens[level];

		GetComponentInChildren<Animator>().SetInteger("Level", Progress.HouseLevel);

		StageGUI.Instance.CreateHealthBar(this);
	}

	protected override void OnTakeDamage(int amount) {
		shaker.Shake(0.7f);

		lastRegen = Time.time;
	}

	protected override void Die() {
		stage.StageLost();
	}

	private void Update() {
		if(Time.time - lastRegen > regen) {
			
			Regen();
			
			lastRegen = Time.time;
		}
	}

	private void Regen() {

		if(CurHealth < MaxHealth) {
			
			CurHealth++;

			regenEffect.Play();

		}
	}
}
