using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerGUI : MonoBehaviour {

	[SerializeField] private Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);
	private Transform target;
	private RectTransform rectTransform;

	public void SetTarget(Transform target) {
		this.target = target;
		this.rectTransform = GetComponent<RectTransform>();
	}

	private void Update() {
		if(target == null) { 
			return;
		}

		rectTransform.anchoredPosition = (Vector2)Camera.main.WorldToScreenPoint(target.position + offset);
	}
}
