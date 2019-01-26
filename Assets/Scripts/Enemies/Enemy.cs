using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Unit {

	private const float DEATH_DELAY = 0.7f;

	private static Treehouse house;

	public float playerStoppingDist = 0.72f;
	public float houseStoppingDist = 1.0f;
	public int damage;
	public float attackRange;
	public float attackPushback;
	public float attackCooldown;
	public GameObject slashPref;

	private bool following = true;
	private float lastAttack;
	private NavMeshAgent agent;
	private EnemyRange range;
	private SpriteRenderer sprite;
	private Unit target;
	private GameObject slash;

	public Unit Target { get { return target; } }

	public void TargetInRange() {
		Attack();
	}

	public void SetTarget(Unit target) {
		agent.stoppingDistance = playerStoppingDist;
		this.target = target;
	}

	protected override void Die() {
		this.enabled = false;

		Invoke("DestroySelf", DEATH_DELAY);
		EventManager.QueueEvent(new EnemyDiedEvent());
	}

	private void DestroySelf() {
		
		LootDropper dropper = GetComponent<LootDropper>();
		if(dropper != null) {
			dropper.Drop();
		}

		Destroy(this.gameObject);
	}

	private void Start() {
		TryFindTreehouse();

		agent = GetComponent<NavMeshAgent>();
		range = GetComponentInChildren<EnemyRange>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		slash = Instantiate(slashPref);
		slash.SetActive(false);

		agent.stoppingDistance = houseStoppingDist;
		target = house;

		EventManager.TriggerEvent(new EnemySpawnedEvent());
	}

	private void TryFindTreehouse() {
		if(house == null) {
			house = GameObject.FindGameObjectWithTag("Treehouse").GetComponent<Treehouse>();
		}
	}

	private void Update() {
		if(!agent.enabled || !following || !Stage.Playing) { return; }

		if(!target.enabled || target.Dead) {
			agent.stoppingDistance = houseStoppingDist;
			target = house;
		}

		agent.SetDestination(target.transform.position);

		if((Time.time - lastAttack) > attackCooldown && range.InRange(target)) {
			Attack();
		}

		sprite.flipX = target.transform.position.x < transform.position.x ? true : false;
	}

	private void Attack() {
		target.TakeDamage(damage);
		Rigidbody enRigidbody = target.GetComponent<Rigidbody>();
		if(enRigidbody != null) {
			enRigidbody.AddForce((target.transform.position - transform.position).normalized * attackPushback, ForceMode.Impulse);
		}
		lastAttack = Time.time;
		following = false;

		slash.SetActive(true);
		slash.transform.SetParent(target.transform);
		slash.transform.localPosition = Vector3.zero;
		
		CustomCoroutine.WaitThenExecute(1.0f, () => {
			 	slash.SetActive(false);
				slash.transform.SetParent(null);
			 }
		);
		CustomCoroutine.WaitThenExecute(attackCooldown, () => following = true);
	}

}
