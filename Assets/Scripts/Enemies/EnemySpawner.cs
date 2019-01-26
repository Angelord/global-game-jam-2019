﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	
    public float breakTimes = 1.0f;
    public List<Transform> spawnPoints = new List<Transform>();
    public List<Wave> waves = new List<Wave>();

    private int waveIndex = 0;
    private int lastSpawnPoint = 0;
    private int livingEnemies;

    private Wave CurrentWave { get { return waves[waveIndex]; } }

    private void Start() {
        StartCoroutine(SpawnNextWave());

        EventManager.AddListener<EnemySpawnedEvent>(HandleEnemySpawnedEvent);
        EventManager.AddListener<EnemyDiedEvent>(HandleEnemyDiedEvent);
    }

    private IEnumerator SpawnNextWave() {
        yield return new WaitForSeconds(breakTimes);

        for(int i = 0; i < CurrentWave.numEnemies; i++) {
            Instantiate(CurrentWave.enemyType, GetNextSpawnPoint().position, Quaternion.identity);
            yield return new WaitForSeconds(CurrentWave.spawnFrequency);
        }
    }   

    private Transform GetNextSpawnPoint() {
        lastSpawnPoint = (lastSpawnPoint + 1) % spawnPoints.Count;
        return spawnPoints[lastSpawnPoint];
    }

    private void HandleEnemySpawnedEvent(EnemySpawnedEvent spawnedEv) {
        livingEnemies++;
    }

    private void HandleEnemyDiedEvent(EnemyDiedEvent diedEv) {
        livingEnemies--;
        if(livingEnemies == 0) {
            StartCoroutine(SpawnNextWave());
        }
    } 
}
