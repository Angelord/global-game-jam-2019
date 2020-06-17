using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	[SerializeField] private float focusSpeed = 5.0f;
	[SerializeField] private Vector2 boundsMin = new Vector2(0.0f, 0.0f);
	[SerializeField] private Vector2 boundsMax = new Vector2(0.0f, 0.0f);
 
	private List<Kid> kids = new List<Kid>();
	private bool focusing = false;
	private Vector3 focusTarget;
	private float halfscreenWidth;
	private float halfscreenHeight;
	private Camera cam;

	public bool FocusedOnTarget {
		get {
			if(!focusing) { return false; }
			else { 
				Vector2 distance = new Vector3(transform.position.x - focusTarget.x, 0.0f, transform.position.z - focusTarget.z);
				return distance.magnitude < 0.0001f;
			}
		}
	}

	public void Focus(Vector3 position) {
		focusTarget = position;
		focusing = true;
	}

	public void StopFocus() {
		focusing = false;
	}

	private void Awake() {
		cam = GetComponent<Camera>();
		
		EventManager.AddListener<KidSpawnedEvent>(HandleKidSpawnedEvent);

		Vector3 topLeft = cam.ScreenToWorldPoint(new Vector2(0.0f, 0.0f));
		Vector3 middle = cam.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));

		halfscreenHeight = middle.z - topLeft.z;
		halfscreenWidth = middle.x - topLeft.x;
		boundsMin.x += halfscreenWidth;
		boundsMax.x -= halfscreenWidth;
		boundsMin.y += halfscreenHeight;
		boundsMax.y -= halfscreenHeight;
	}

	private void OnDestroy() {
		EventManager.RemoveListener<KidSpawnedEvent>(HandleKidSpawnedEvent);
	}

	private void Update() {
		if(kids.Count == 0) { return; }

		
		if(focusing) {

			MoveTowards(new Vector3(focusTarget.x, transform.position.y, focusTarget.z));
		}
		else {
			Vector3 center = Vector3.zero;

			int validKids = 0;
			foreach(var kid in kids) {
				if(kid != null && !kid.Dead) {
					center += kid.transform.position;
					validKids++;
				}
			}

			if(validKids == 0) {
				return;
			}

			center /= validKids;

			Vector3 targetPos = new Vector3(center.x, transform.position.y, center.z);
			MoveTowards(targetPos);
		}
	}

	private void MoveTowards(Vector3 targetPos) {

		float mvmStep = focusSpeed * Time.deltaTime;
		
		Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, mvmStep);

		if(newPos.x < boundsMin.x || newPos.x > boundsMax.x) {
			newPos.x = transform.position.x;
		}

		if(newPos.z < boundsMin.y || newPos.z > boundsMax.y) {
			newPos.z = transform.position.z;
		}

		transform.position = newPos;

	}

	private void HandleKidSpawnedEvent(KidSpawnedEvent kidSpawned) {
		kids.Add(kidSpawned.Kid);
	}
}
