using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	[SerializeField] private GameObject namePrefab;
	
	private void Start() {
		GameObject gui = GameObject.FindGameObjectWithTag("GUI");

		GameObject name = Instantiate(namePrefab, gui.transform);
		name.GetComponent<FollowerGUI>().SetTarget(this.transform);
	}
}
