using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpritePositionSorter : MonoBehaviour {
	
	private void Update() {
		this.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.z);
	}
}
