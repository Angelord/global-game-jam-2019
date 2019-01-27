using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : MonoBehaviour {

	[SerializeField] private Sprite icon;
	private Kid kid;

	public Sprite Icon { get { return icon; } } 
	protected Kid Kid { get { return kid; } }

	private void Awake() {
		kid = GetComponentInParent<Kid>();
	}

	public abstract void Use();
}
