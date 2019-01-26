using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counterbar : MonoBehaviour {

	public List<GameObject> counters = new List<GameObject>();

	public void SetColor(Color col) {
		foreach(var counter in counters) {
			counter.GetComponent<Image>().color = col;
		}
	}

	public void SetValue(int val) {
		if(val > counters.Count) {
			int toCreate = val - counters.Count;
			for(int i = 0; i < toCreate; i++) {
				Instantiate(counters[0], this.transform);
			}
		}

		foreach(var counter in counters) {
			counter.SetActive(false);
		}

		for(int i = 0; i < val; i++) {
			counters[i].SetActive(true);
		}
	}
}
