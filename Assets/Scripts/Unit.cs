using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	[SerializeField]private int health;
	private int curHealth;

	public int MaxHealth { get { return health; } }
	public int CurHealth { get { return curHealth; } protected set { curHealth =  Mathf.Clamp(value, 0, MaxHealth); } }
	public bool Dead { get { return curHealth <= 0; } }
	
	public void SetMaxHealth(int val) {
		this.health = val;
		this.curHealth = Mathf.Clamp(curHealth, 0, health);
	}

	public void TakeDamage(int amount) {
		if(Dead) { return; }

		OnTakeDamage(amount);
		curHealth -= amount;
		if(curHealth <= 0) {
			Die();
		}
	}

	public abstract void Die();
	
	protected void ResetHealth() {
		curHealth = health;
	}

	protected virtual void OnTakeDamage(int amount) { }

	private void Awake() {
		curHealth = health;
		OnAwake();
	}

	protected virtual void OnAwake() {
		
	}

}
