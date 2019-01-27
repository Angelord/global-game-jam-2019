using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {


	[SerializeField] private GameObject root;
	[SerializeField] private Image[] usableIcons;

	private List<Kid> kids = new List<Kid>();

	private void Start() {
		root.SetActive(false);
		EventManager.AddListener<StageStartedEvent>(HandleStageStartedEvent);
		EventManager.AddListener<KidSpawnedEvent>(HandleKidSpawnedEvnet);
	}

	private void HandleStageStartedEvent(StageStartedEvent startedEv) { 
		root.SetActive(true);
	} 

	private void HandleKidSpawnedEvnet(KidSpawnedEvent kidSpawnedEvent) {
		kids.Add(kidSpawnedEvent.Kid);
	}

	private void Update() {

		if(!root.activeSelf) {
			return;
		}

		if(!Stage.Playing) {
			root.SetActive(false);
		}

		for(int i = 0; i < kids.Count; i++) {
			Image icon = usableIcons[i];
			if(icon.sprite != kids[i].CurUsable.Icon) {
				//TODO : Lerp alpha
				icon.sprite = kids[i].CurUsable.Icon;
			}
		}

	}
}
