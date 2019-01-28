using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTreehouse : MonoBehaviour {

	private Animator animator;

	private void Start() {
		animator = GetComponentInChildren<Animator>();
	}

	private void Update() {
		if(animator.GetInteger("Level") != Progress.HouseLevel) {
			animator.SetInteger("Level", Progress.HouseLevel);
		}
	}
}
