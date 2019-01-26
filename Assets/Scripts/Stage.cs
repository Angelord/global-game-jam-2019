﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {

	private const float END_GAME_DELAY = 2.0f;


	[SerializeField] private string[] startOfGameLines;
	[SerializeField] private SpeechBubble momSpeech; 
	[SerializeField] private Gate gate;
	[SerializeField] private GameObject gameOverScreen;

	private GameState state = GameState.Playing;
	private CameraControl cameraControl; 
	private List<Kid> kids = new List<Kid>();
	
	private static Stage instance;

	public static bool Playing { get { return instance.state == GameState.Playing; } }

	private void Start() {
		instance = this;

		cameraControl = Camera.main.GetComponent<CameraControl>();

		momSpeech.gameObject.SetActive(false);

		AudioController.Instance.SetLoop("battle_main");
        AudioController.Instance.SetLoopVolume(0f);
        AudioController.Instance.FadeInLoop(0.2f, 0.75f);

		EventManager.AddListener<KidSpawnedEvent>(HandleKidSpawnedEvent);

		StartCoroutine(OnStageStarting());
	}

	private void OnDestroy() {
		if(instance == this) { 
			instance = null;
		}
	}

	public void StageOver() {
		if(!Playing) { return; }

		state = GameState.Victory;
		StartCoroutine(OnStageOver());
	}

	public void GameOver() {
		if(!Playing) { return; }

		state = GameState.Lost;
		Treehouse house = GameObject.FindGameObjectWithTag("Treehouse").GetComponent<Treehouse>();
		cameraControl.Focus(house.transform.position);
		CustomCoroutine.WaitThenExecute( END_GAME_DELAY, () => gameOverScreen.SetActive(true));
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void HandleKidSpawnedEvent(KidSpawnedEvent kidSpawned) {
		kids.Add(kidSpawned.Kid);
	}

	private IEnumerator OnStageStarting() {
		yield return new WaitForSeconds(0.5f);

		kids[Random.Range(0, kids.Count )].Say(startOfGameLines[Random.Range(0, startOfGameLines.Length)]);
	}

	private IEnumerator OnStageOver() {

		Kid.Locked = true;

		cameraControl.Focus(gate.transform.position);

		yield return new WaitUntil(() => cameraControl.FocusedOnTarget);

		momSpeech.Show("Hank! James! Dinner is ready!");

		yield return new WaitUntil( () => !momSpeech.Typing );

		gate.Open();

		yield return new WaitForSeconds(2.4f);

		cameraControl.StopFocus();

		Kid.Locked = false;
	}
}

public enum GameState {
	Playing,
	Lost,
	Victory
}