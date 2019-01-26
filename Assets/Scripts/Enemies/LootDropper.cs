using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour {

	[SerializeField] private GameObject lootPrefab;

	public void Drop() {
		if(lootPrefab != null) {
			Instantiate(lootPrefab, transform.position, Quaternion.identity);
			Destroy(this);
		}
	}
}
