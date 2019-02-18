using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	[SerializeField] private GameObject resumeBtn;
	[SerializeField] private GameObject continueBtn;
	[SerializeField] private GameObject newGameBtn;
	[SerializeField] private GameObject quitBtn;
	[SerializeField] private Stage stage;
	[SerializeField] private GameObject playerHUD;
	[SerializeField] private GameObject tutorial;
	[SerializeField] private GameObject credits;

	private static bool paused = true;
	private GameObject root;

	private void Start() {
		root = transform.GetChild(0).gameObject;

		if(Progress.GameStarted) {

			stage.StartStage();

			paused = false;

			root.SetActive(false);
		}
		else {

			AudioController.Instance.SetLoop("spooky_2");
			AudioController.Instance.SetLoopVolume(0.0f);
			AudioController.Instance.FadeInLoop(0.3f, 0.8f);
		}

		continueBtn.SetActive(false);
		resumeBtn.SetActive(false);
	}

	public void HowToPlay() {
		tutorial.SetActive(!tutorial.activeSelf);
		credits.SetActive(!tutorial.activeSelf);
	}
	
	public void Credits() {
		credits.SetActive(!credits.activeSelf);
		tutorial.SetActive(!credits.activeSelf);
	}

	public void Resume() {
		TogglePause();
	}

	public void Continue() { }

	public void NewGame() {
		Progress.Reset();
		
		Progress.GameStarted = true;

		if(Stage.Playing) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else {
			stage.StartStage();
		}
		
		Resume();
	}

	public void Quit() {
		Application.Quit();
	}

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Escape) && Stage.Playing) {
			TogglePause();
		}
	}

	private void TogglePause() {

		paused = !paused;

		Time.timeScale = paused ? 0.0f : 1.0f;
		
		root.SetActive(paused);

		if(paused) {
			AudioController.Instance.PauseLoop();
			resumeBtn.SetActive(Stage.Playing); 
		}

		if(!paused) {
			AudioController.Instance.ResumeLoop();
			tutorial.SetActive(false);
			credits.SetActive(false);
		}

		playerHUD.SetActive(!paused);
	}
}