using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private List<Kid> kids = new List<Kid>();

	private void Awake() {
		EventManager.AddListener<KidSpawnedEvent>(HandleKidSpawnedEvent);
	}

	private void Update() {
		if(kids.Count == 0) { return; }

		Vector3 center = Vector3.zero;
		foreach(var kid in kids) {
			center += kid.transform.position;
		}

		center /= kids.Count;

		transform.position = new Vector3(center.x, transform.position.y, center.z);
	}

	private void HandleKidSpawnedEvent(KidSpawnedEvent kidSpawned) {
		kids.Add(kidSpawned.Kid);
	}

	private void HandleKidDiedEvent(KidDiedEvent kidDied) {
		kids.Remove(kidDied.Kid);
	}
}
