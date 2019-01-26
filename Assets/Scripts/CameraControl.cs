using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	[SerializeField] private float focusSpeed = 5.0f;

	private List<Kid> kids = new List<Kid>();
	private bool focusing = false;
	private Vector3 focusTarget;

	public void Focus(Vector3 position) {
		focusTarget = position;
		focusing = true;
	}

	public void StopFocus() {
		focusing = false;
	}

	private void Awake() {
		EventManager.AddListener<KidSpawnedEvent>(HandleKidSpawnedEvent);
	}

	private void Update() {
		if(kids.Count == 0) { return; }

		float mvmStep = focusSpeed * Time.deltaTime;
		
		if(focusing) {

			transform.position = Vector3.MoveTowards(transform.position, new Vector3(focusTarget.x, transform.position.y, focusTarget.z), mvmStep);
		}
		else {
			Vector3 center = Vector3.zero;
			foreach(var kid in kids) {
				center += kid.transform.position;
			}

			center /= kids.Count;

			transform.position = Vector3.MoveTowards(transform.position, new Vector3(center.x, transform.position.y, center.z), mvmStep);
		}
	}

	private void HandleKidSpawnedEvent(KidSpawnedEvent kidSpawned) {
		kids.Add(kidSpawned.Kid);
	}

	private void HandleKidDiedEvent(KidDiedEvent kidDied) {
		kids.Remove(kidDied.Kid);
	}
}
