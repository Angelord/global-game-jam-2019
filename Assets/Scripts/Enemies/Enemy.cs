using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Unit {

	private static Treehouse house;

	public float playerStoppingDist = 0.72f;
	public float houseStoppingDist = 1.0f;
	public int damage;
	public float attackPushback;
	public float attackCooldown;
	public GameObject slashPref;
	public bool hasDeathAnim = false; 
	public float deathDelay = 0.7f;

	private bool following = true;
	private float lastAttack;
	private NavMeshAgent agent;
	private EnemyRange range;
	private SpriteRenderer sprite;
	private Unit target;
	private GameObject slash;

	protected float LastAttack { get { return lastAttack; } set { lastAttack = value; } }
	protected bool Following { get { return following; } set { following = value; } }
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
		if(hasDeathAnim) {
			GetComponentInChildren<Animator>().SetTrigger("Die");
		}
		
		agent.enabled = false;
		Invoke("DestroySelf", deathDelay);
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

		if(slashPref != null) {
			slash = Instantiate(slashPref);
			slash.SetActive(false);
		}

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
		if(!agent.isActiveAndEnabled || !following || !Stage.Playing) { return; }

		if(target.gameObject == null || !target.enabled || target.Dead) {
			agent.stoppingDistance = houseStoppingDist;
			target = house;
		}

		agent.SetDestination(target.transform.position);

		if((Time.time - lastAttack) > attackCooldown && range.InRange(target)) {
			Attack();
		}

		sprite.flipX = target.transform.position.x < transform.position.x ? true : false;
	}

	protected virtual void Attack() {
		target.TakeDamage(damage);
		Rigidbody enRigidbody = target.GetComponent<Rigidbody>();
		if(enRigidbody != null) {
			enRigidbody.AddForce((target.transform.position - transform.position).normalized * attackPushback, ForceMode.Impulse);
		}
		lastAttack = Time.time;
		following = false;

		if(slashPref != null) {
			slash.SetActive(true);
			slash.transform.SetParent(target.transform);
			slash.transform.localPosition = Vector3.zero;
			CustomCoroutine.WaitThenExecute(1.0f, () => {
					if(slash != null) {
						slash.SetActive(false);
						slash.transform.SetParent(null);
					}
				}
			);
		}

		CustomCoroutine.WaitThenExecute(attackCooldown, () => following = true);
	}

}
