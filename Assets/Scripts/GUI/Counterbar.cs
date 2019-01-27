using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counterbar : MonoBehaviour {

	public List<Image> counters = new List<Image>();
	public Sprite regularSprite;
	public Sprite stunnedSprite;

	public bool Stunned {
		set {
			if(value) {
				foreach(var counter in counters) {
					counter.sprite = stunnedSprite;
				}
			}
			else {
				foreach(var counter in counters) {
					counter.sprite = regularSprite;
				}
			}
		}

	}

	public void SetValue(int val) {

		foreach(var counter in counters) {
			counter.color = Color.black;
		}

		for(int i = 0; i < val; i++) {
			counters[i].color = Color.white;
		}
	}
}
