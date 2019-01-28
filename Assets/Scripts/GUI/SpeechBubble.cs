using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

	
	[SerializeField] private float typePause = 0.05f;
	[SerializeField] private float punctuationPause = 0.1f;
	[SerializeField] private float disableDelay = 3.0f;
	[SerializeField] private float textPaddingHeight = 12;
	private Text uiText;
	private RectTransform rectTransform;
	private bool typing = false;

	public bool Typing { get { return typing; } }

	private void Start() {
		FindDependencies();

		this.gameObject.SetActive(false);
	}

	public void Show(string text) {
		if(string.IsNullOrEmpty(text)) { return; }

		if(uiText == null) {
			FindDependencies();
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
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, uiText.preferredHeight + textPaddingHeight * 2);
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

	private void FindDependencies() {

		uiText = GetComponentInChildren<Text>();

		rectTransform = GetComponent<RectTransform>();
	}

}
