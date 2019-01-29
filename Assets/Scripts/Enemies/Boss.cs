using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	[SerializeField] private GameObject namePrefab;
	[SerializeField] private string name;

	private void Start() {
		GameObject gui = GameObject.FindGameObjectWithTag("GUI");

		GameObject nameGUI = Instantiate(namePrefab, gui.transform);
		nameGUI.GetComponentInChildren<Text>().text = name;
		nameGUI.GetComponent<FollowerGUI>().SetTarget(this.transform);
	}
}
