using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {

	private const float END_GAME_DELAY = 2.0f;

	[SerializeField] private Gate gate;
	[SerializeField] private GameObject gameOverScreen;

	[Header("Music")]
	[SerializeField] private AudioClip battleLoop; 

	private GameState state = GameState.Playing;
	private CameraControl cameraControl; 
	
	private static Stage instance;

	public static bool Playing { get { return instance.state == GameState.Playing; } }

	private void Start() {
		instance = this;

		cameraControl = Camera.main.GetComponent<CameraControl>();

		AudioController.Instance.SetLoop("battle_main");
        AudioController.Instance.SetLoopVolume(0f);
        AudioController.Instance.FadeInLoop(0.2f, 0.75f);
	}

	private void OnDestroy() {
		if(instance == this) { 
			instance = null;
		}
	}

	public void StageOver() {
		state = GameState.Victory;
		StartCoroutine(OnStageOver());
	}

	public void GameOver() {
		state = GameState.Lost;
		Treehouse house = GameObject.FindGameObjectWithTag("Treehouse").GetComponent<Treehouse>();
		cameraControl.Focus(house.transform.position);
		CustomCoroutine.WaitThenExecute( END_GAME_DELAY, () => gameOverScreen.SetActive(true));
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private IEnumerator OnStageOver() {
		Kid.Locked = true;

		cameraControl.Focus(gate.transform.position);

		yield return new WaitUntil(() => cameraControl.FocusedOnTarget);

		//TODO : Show mom dialogue

		gate.Open();

		yield return new WaitForSeconds(1.5f);

		cameraControl.StopFocus();

		Kid.Locked = false;
	}
}

public enum GameState {
	Playing,
	Lost,
	Victory
}
