using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGUI : MonoBehaviour {


	[SerializeField] private GameObject healthbar;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject endGameScreen;

	private static StageGUI instance;

	public static StageGUI Instance { get { return instance; } }

	private void Awake() {
		instance = this;
	}

	private void Update() {
		pauseMenu.transform.SetAsLastSibling();
		endGameScreen.transform.SetAsFirstSibling();
	}

	public Healthbar CreateHealthBar(Unit unit) {

		Healthbar newBar = Instantiate(healthbar, this.transform).GetComponent<Healthbar>();

		newBar.SetTarget(unit);

		return newBar;
	}
}
