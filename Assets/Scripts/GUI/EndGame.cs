using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public LerpAlpha thankYouAlpha;

	private void Start () {
		thankYouAlpha.IntendedAlpha = 1.0f;
		ScreenEffects.FadeIn(3.0f);
	}

	public void NewGame() {
		Progress.Reset();
		ScreenEffects.FadeOut(1.2f);
		
		CustomCoroutine.WaitThenExecute(1.2f, () => { SceneManager.LoadScene("Treehouse"); } );
	}

	public void Quit() {
		ScreenEffects.FadeOut(0.7f);
		CustomCoroutine.WaitThenExecute(0.7f, () => { Application.Quit(); } );
	}
	
}
