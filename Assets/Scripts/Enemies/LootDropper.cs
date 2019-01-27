using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : MonoBehaviour {

	[SerializeField] private GameObject lootPrefab;
	[SerializeField] private int minAmount = 1;
	[SerializeField] private int maxAmount = 1;

	public void Drop() {
		if(lootPrefab != null) {
			int amountToDrop = Random.Range(minAmount, maxAmount + 1);
			for(int i = 0; i < amountToDrop; i++) {

				float xOffset = Random.Range(-1.0f, 1.0f);
				float zOffset = Random.Range(-1.0f, 1.0f);
				Vector3 spawnPos = transform.position + new Vector3(xOffset, 0.0f, zOffset);

				GameObject loot = Instantiate(lootPrefab, spawnPos, Quaternion.identity);

				Vector3 forceDir = spawnPos - transform.position;
				forceDir.Normalize();
				
				loot.GetComponent<Rigidbody>().AddForce(forceDir * 1.9f, ForceMode.Impulse);
			}
			Destroy(this);
		}
	}
}
