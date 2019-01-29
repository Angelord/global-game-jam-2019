using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
	
    [SerializeField] private float breakTimes = 5.0f;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private Stage stage;
    [SerializeField] private WaveData waveData;
    [SerializeField] private Text notifyText;

    private List<Wave> waves = new List<Wave>();
    private int waveIndex = 0;
    private int lastSpawnPoint = 0;
    private int livingEnemies;

    private Wave CurrentWave { get { return waves[waveIndex]; } }

    private void Start() {
        
        EventManager.AddListener<StageStartedEvent>(HandleStageStartedEvent);
        EventManager.AddListener<EnemySpawnedEvent>(HandleEnemySpawnedEvent);
        EventManager.AddListener<EnemyDiedEvent>(HandleEnemyDiedEvent);
    }

    private void HandleStageStartedEvent(StageStartedEvent startedEvent) {

        waves = waveData.GetWavesForDay(Progress.Day);

        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave() {

        yield return new WaitForSeconds(breakTimes);

        stage.WaveStarted(waveIndex);

        LerpAlpha textAplha = notifyText.GetComponent<LerpAlpha>();
        notifyText.text = "Wave " + (waveIndex + 1);
        textAplha.SetAlpha(0.0f);
        textAplha.IntendedAlpha = 0.8f;
        CustomCoroutine.WaitThenExecute(1.54f, ()=> {
            textAplha.IntendedAlpha = 0.0f;
        });

        foreach(var spawnGroup in CurrentWave.spawnGroups) {
            StartCoroutine(SpawnGroup(spawnGroup));
        }
    }   

    private IEnumerator SpawnGroup(SpawnGroup group) {

        for(int i = 0; i < group.numEnemies; i++) {
            yield return new WaitForSeconds(group.spawnFreq);
            Instantiate(group.enemy, GetNextSpawnPoint().position, Quaternion.identity);
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
            waveIndex++;
            if(waveIndex == waves.Count) {
                stage.StageOver();
            }
            else {
                stage.WaveOver(waveIndex - 1);
                StartCoroutine(SpawnNextWave());
            }
        }
    } 
}
