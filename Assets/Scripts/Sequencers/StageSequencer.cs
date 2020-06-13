using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class StageSequencer : MonoBehaviour {

	private SequencerData data;
	private Stage stage;
	private CameraControl cameraControl;
	private string[] startOfGameLines = {
		"Let's do this!",
		"Pew! Pew!",
		"Defend the base!",
		"There's no end to them!",
		"We've got this!",
		"Be careful, bro."
	};

	private string[] momLines = {
		"Hank! Phil! Dinner is ready!",
		"It's getting late boys!",
		"Home. Now. Don't make me shout"
	};

	private void Awake() {
		data = GetComponent<SequencerData>();
		stage = GetComponent<Stage>();
		cameraControl = Camera.main.GetComponent<CameraControl>(); 
	}

	public virtual IEnumerator OnStageStarted(System.Action onReady) {

		yield return new WaitForSeconds(0.5f);

		stage.Kids[Random.Range(0, 2 )].Say(startOfGameLines[Random.Range(0, startOfGameLines.Length)]);

		onReady();
	}

	public virtual IEnumerator OnWaveOver(int wave) {
		yield return 0;
	}

	public virtual IEnumerator OnWaveStarted(int wave) {
		yield return 0;
	}

	public virtual IEnumerator OnStageOver(System.Action onReady) {

		AudioController.Instance.FadeOutLoop(0.35f);

		yield return new WaitForSeconds(2.0f);

		Kid.Locked = true;

		cameraControl.Focus(data.gate.transform.position);

		yield return new WaitUntil(() => cameraControl.FocusedOnTarget);

		AudioController.Instance.SetLoopVolume(0.0f);
		AudioController.Instance.SetLoop("victory");
		AudioController.Instance.FadeInLoop(0.35f, 0.8f);

		EventManager.TriggerEvent(new MomSaidEvent());
		data.momSpeech.Show(momLines[Random.Range(0, momLines.Length)]);

		yield return new WaitUntil( () => !data.momSpeech.isActiveAndEnabled );

		data.gate.Open();

		yield return new WaitForSeconds(1.0f);

		cameraControl.StopFocus();

		Kid.Locked = false;

		onReady();
	}
}
