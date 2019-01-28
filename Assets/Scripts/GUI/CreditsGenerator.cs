using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsGenerator : MonoBehaviour {

	public List<string> names = new List<string>();

	private void Start() {

		Text text = GetComponent<Text>();

		for(int i = names.Count - 1; i >= 0; i--) {
			int name = Random.Range(0, names.Count);
			text.text += names[name] + "\n";
			names.RemoveAt(name);
		}
	}
}
