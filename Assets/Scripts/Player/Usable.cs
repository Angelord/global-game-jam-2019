using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : MonoBehaviour {

	private Kid kid;

	protected Kid Kid { get { return kid; } }

	private void Awake() {
		kid = GetComponentInParent<Kid>();
	}

	public void Use() {
		
	}
}
