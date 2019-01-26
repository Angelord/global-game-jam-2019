using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

	
	[SerializeField] private float typePause = 0.05f;
	[SerializeField] private float punctuationPause = 0.1f;
	[SerializeField] private float disableDelay = 3.0f;
	private Text uiText;
	private bool typing = false;

	public bool Typing { get { return typing; } }

	private void Start() {
		this.gameObject.SetActive(false);
	}

	public void Show(string text) {
		if(string.IsNullOrEmpty(text)) { return; }

		if(uiText == null) {
			uiText = GetComponentInChildren<Text>();
		}

		this.gameObject.SetActive(true);
		StopAllCoroutines();
		StartCoroutine(DoShow(text));
	}

	private IEnumerator DoShow(string text) {
		typing = true;
		uiText.text = "";
		for(int i = 0; i < text.Length; i++) {
			uiText.text += text[i];
			if(text[i] != '!' && text[i] != '.' && text[i] != '?') {
				yield return new WaitForSeconds(typePause);
			}
			else {
				yield return new WaitForSeconds(punctuationPause);
			}
		}

		typing = false;

		yield return new WaitForSeconds(disableDelay);
		this.gameObject.SetActive(false);
	}
}
