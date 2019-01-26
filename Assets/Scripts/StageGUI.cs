using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGUI : MonoBehaviour {


	[SerializeField] private GameObject healthbar;

	private static StageGUI instance;

	public static StageGUI Instance { get { return instance; } }

	private void Start() {
		instance = this;
	}

	public Healthbar CreateHealthBar(Unit unit) {

		Healthbar newBar = Instantiate(healthbar, this.transform).GetComponent<Healthbar>();

		newBar.SetTarget(unit);

		return newBar;
	}
}
