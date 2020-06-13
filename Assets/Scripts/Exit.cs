using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	[SerializeField] private float fadeOutDuration = 0.7f;

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			
			Progress.DayPassed();
		
			StartCoroutine(TransitionScene());
		}
	}

	private IEnumerator TransitionScene() {

		Kid.Locked = true;

		ScreenEffects.FadeOut(fadeOutDuration);

		yield return new WaitForSeconds(fadeOutDuration);

		Kid.Locked = false;

		EventManager.TriggerEvent(new ShopEnteredEvent());
		if(Progress.Day > Progress.LAST_DAY) {
			SceneManager.LoadScene("EndScene");
		}
		else {
			SceneManager.LoadScene("ShopNew");
		}
	}
}
