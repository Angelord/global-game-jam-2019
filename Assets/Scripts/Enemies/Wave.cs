using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
	public string songLoop;
	public string name;
	public float delay = 3.0f;
	public List<SpawnGroup> spawnGroups;

}

[System.Serializable] 
	public class SpawnGroup {
		public GameObject enemy;
		public float spawnFreq;
		public int numEnemies;
	}

