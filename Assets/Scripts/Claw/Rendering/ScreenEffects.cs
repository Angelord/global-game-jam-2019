using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Jitterer))]
public class ScreenEffects : MonoBehaviour {

	private static ScreenEffects instance;

	[SerializeField] private Material renderMat;

	private Jitterer jitterer;
    private Texture2D overlay;

	public static void Jitter(float duration = 0.8f) {
		instance.jitterer.Jitter(duration);
	}

	public static void FadeOut(float duration) {
		instance.StartCoroutine(instance.DoFade(true, duration));
	}

	public static void FadeIn(float duration) {
		instance.StartCoroutine(instance.DoFade(false, duration));
	}

    private void Start(){
		instance = this;

		overlay = new Texture2D (1, 1);
		overlay.SetPixel (0, 0, new Color(0,0,0,0));
		overlay.Apply ();

		jitterer = GetComponent<Jitterer>();
    }

	private void OnGUI(){
		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), overlay);
	}

	private void OnDestroy() {
		instance = null;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination) {
		if(renderMat != null) {
        	Graphics.Blit(source, destination, renderMat);
		}
		else {
			Graphics.Blit(source, destination);
		}
    }

	private IEnumerator DoFade(bool fadeOut, float duration) {
		float timeRemaining = duration;

		while(timeRemaining >= 0.0f) {

			float alpha = fadeOut ? (1.0f - timeRemaining/duration) : timeRemaining / duration;
			overlay.SetPixel(0, 0, new Color(0, 0, 0, alpha));
			overlay.Apply();

			timeRemaining -= Time.deltaTime;

			yield return 0;
		}
	}
}
