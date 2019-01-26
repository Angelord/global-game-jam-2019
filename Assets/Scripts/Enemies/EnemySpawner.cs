using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	
    public float breakTimes = 1.0f;
    public List<Wave> waves = new List<Wave>();

    private int waveIndex = 0;
    private int enemiesDead;

    private Wave CurrentWave { get { return waves[waveIndex]; } }

    private void Start() {
        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave() {
        yield return new WaitForSeconds(breakTimes);

        for(int i = 0; i < CurrentWave.numEnemies; i++) {
            Instantiate(CurrentWave.enemyType, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(CurrentWave.spawnFrequency);
        }
    }   
}
