using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour {

	[SerializeField] private Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);
	private Unit target;
	private RectTransform rectTransform;
	private Slider slider;

	public void SetTarget(Unit target) {
		slider = GetComponent<Slider>();
		slider.maxValue = target.MaxHealth;

		this.target = target;
		this.rectTransform = GetComponent<RectTransform>();
	}

	private void Update() {
		if(target == null) { 
			Destroy(this.gameObject); 
		}

		rectTransform.anchoredPosition = (Vector2)Camera.main.WorldToScreenPoint(target.transform.position + offset);
		slider.value = target.CurHealth;
	}
}
