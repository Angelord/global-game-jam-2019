using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			
			Progress.DayPassed();
			//TODO : Go to teleshop
			SceneManager.LoadScene("Shop");
		}
	}
}
