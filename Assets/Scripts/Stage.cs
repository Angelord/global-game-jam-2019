using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {

	public GameObject gameOverScreen;
	private GameState state = GameState.Playing;
	
	private static Stage instance;

	public static bool Playing { get { return instance.state == GameState.Playing; } }

	private void Start() {
		if(instance == null) {
			instance = this;
		}
		else {
			Destroy(this.gameObject);
		}
	}

	private void OnDestroy() {
		if(instance == this) { 
			instance = null;
		}
	}

	public void GameOver() {
		state = GameState.Lost;
		gameOverScreen.SetActive(true);
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}

public enum GameState {
	Playing,
	Lost,
	Victory
}
