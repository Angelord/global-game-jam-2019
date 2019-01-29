using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	[SerializeField] private GameObject namePrefab;
	[SerializeField] private string name;
	[SerializeField] private string bossTheme;

	private void Start() {
		GameObject gui = GameObject.FindGameObjectWithTag("GUI");

		GameObject nameGUI = Instantiate(namePrefab, gui.transform);
		nameGUI.GetComponentInChildren<Text>().text = name;
		nameGUI.GetComponent<FollowerGUI>().SetTarget(this.transform);

		if(!string.IsNullOrEmpty(bossTheme)) {
			AudioController.Instance.CrossFadeLoopIfNotPlaying(bossTheme, 0.8f, 0.4f);
		}
	}
}
