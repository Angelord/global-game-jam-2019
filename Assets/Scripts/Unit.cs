using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	[SerializeField]private int health;
	private int curHealth;

	public int MaxHealth { get { return health; } }
	public int CurHealth { get { return curHealth; } protected set { curHealth =  Mathf.Clamp(value, 0, MaxHealth); } }
	public bool Dead { get { return curHealth <= 0; } }
	
	public void TakeDamage(int amount) {
		if(Dead) { return; }

		OnTakeDamage(amount);
		curHealth -= amount;
		if(curHealth <= 0) {
			Die();
		}
	}

	protected void ResetHealth() {
		curHealth = health;
	}

	protected virtual void OnTakeDamage(int amount) { }

	protected abstract void Die();

	private void Awake() {
		curHealth = health;
	}

}
