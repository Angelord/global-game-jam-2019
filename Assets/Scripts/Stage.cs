using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour {

	private const float END_GAME_DELAY = 2.0f;

	[SerializeField] private GameObject kid1Pref;
	[SerializeField] private GameObject kid2Pref;
	[SerializeField] private Transform[] kidSpawns = new Transform[2];
	[SerializeField] private GameObject gameOverScreen;
	[SerializeField] private Text dayText;
	[SerializeField] private StageSequencer[] sequencers = new StageSequencer[Progress.LAST_DAY];

	private GameState state = GameState.InMenu;
	private CameraControl cameraControl; 
	private List<Kid> kids = new List<Kid>();
	private StageSequencer sequencer;
	
	private static Stage instance;

	public List<Kid> Kids { get { return kids; } }
	public static bool Playing { get { return instance.state == GameState.Playing; } }

	private void Awake() {

		instance = this;

		sequencer = sequencers[Progress.Day];
	}

	private void Start() {
		ScreenEffects.FadeIn(1.6f);
		AudioController.Instance.SetLoopVolume(0.0f);
		cameraControl = Camera.main.GetComponent<CameraControl>();
	}

	private void OnDestroy() {

		if(instance == this) { 

			instance = null;
		}
	}

	public void StartStage() {
		Debug.Log("Starting stage");
		if(Progress.Day == 0) {
			Progress.ModAmmo(UsableType.Dummy, +1);
			Progress.ModAmmo(UsableType.Banana, +3);
		}

		GameObject player1 = Instantiate(kid1Pref, kidSpawns[0].position, Quaternion.identity);
		kids.Add(player1.GetComponent<Kid>());

		GameObject player2 = Instantiate(kid2Pref, kidSpawns[1].position, Quaternion.identity);
		kids.Add(player2.GetComponent<Kid>());

		dayText.gameObject.SetActive(true);
		dayText.text = "Day " + (Progress.Day + 1);
		dayText.GetComponent<LerpAlpha>().SetAlpha(0.0f);
		dayText.GetComponent<LerpAlpha>().IntendedAlpha = 1.0f;
		CustomCoroutine.WaitThenExecute(3.0f, () => dayText.GetComponent<LerpAlpha>().IntendedAlpha = 0.0f);

		sequencer.StartCoroutine(sequencer.OnStageStarted(
			() => {
				state = GameState.Playing;
				EventManager.QueueEvent(new StageStartedEvent());
				StageOver();
			}
		));


	}

	public void StageOver() {

		if(!Playing) { return; }

		state = GameState.Victory;

		sequencer.StartCoroutine(sequencer.OnStageOver(() => {}));
	}

	public void WaveOver(int index) {
		sequencer.StartCoroutine(sequencer.OnWaveOver(index));
	}

	public void WaveStarted(int index) {
		sequencer.StartCoroutine(sequencer.OnWaveStarted(index));
	}

	public void StageLost() {
		if(!Playing) { return; }

		state = GameState.Lost;

		Treehouse house = GameObject.FindGameObjectWithTag("Treehouse").GetComponent<Treehouse>();
		cameraControl.Focus(house.transform.position);
	
		CustomCoroutine.WaitThenExecute( END_GAME_DELAY, () => gameOverScreen.SetActive(true));
	}

	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	
}

public enum GameState {
	InMenu,
	Playing,
	Lost,
	Victory
}
