using System.Collections;
using System.Collections.Generic;
using Events;
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
	private LerpAlpha[] alphaLerpers;
	private float defaultAlpha;

	public bool Typing { get { return typing; } }

	private void Start() {
		FindDependencies();

		this.gameObject.SetActive(false);
	}

	public void Show(string text) {
		if(string.IsNullOrEmpty(text)) { return; }

		if(uiText == null) { FindDependencies(); }
		
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

		for(int i = 0; i < alphaLerpers.Length; i++) {
			alphaLerpers[i].IntendedAlpha = 0.0f;
		}

		yield return new WaitForSeconds(disableDelay);
		this.gameObject.SetActive(false);

		for(int i = 0; i < alphaLerpers.Length; i++) {
			alphaLerpers[i].SetAlpha(defaultAlpha);
		}
	}

	private void FindDependencies() {
		if(uiText != null) { return; }
		uiText = GetComponentInChildren<Text>();
		rectTransform = GetComponent<RectTransform>();
		defaultAlpha = GetComponent<Image>().color.a;
		alphaLerpers = GetComponentsInChildren<LerpAlpha>();
	}

}
