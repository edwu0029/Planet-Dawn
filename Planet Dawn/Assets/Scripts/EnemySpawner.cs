using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [Header("Waves")] //Wave Variables
    [SerializeField] List<WaveConfig> waves;
    [SerializeField] int startingWaveIndex = 0;
    [SerializeField] bool isStrikerSpawner = false;

    bool finishedSpawningEnemiesInWave = false;

    void Start() {
        
    }
    void Update() {
        
    }

    public IEnumerator SpawnAllWaves() {
        for(int waveIndex = startingWaveIndex; waveIndex < waves.Count; waveIndex++) {
            yield return StartCoroutine(SpawnEnemiesInWave(waveIndex));
        }
    }
    public IEnumerator SpawnEnemiesInWave(int waveIndex) {
        finishedSpawningEnemiesInWave = false;

        var currentWave = waves[waveIndex];
        for (int enemyCount = 1; enemyCount <= currentWave.GetNumEnemies(); enemyCount++) {
            var newEnemy = Instantiate(currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].GetPosition(),
                transform.rotation);
            newEnemy.GetComponent<EnemyMovement>().SetWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }

        finishedSpawningEnemiesInWave = true;
    }
    public void SpawnAllWavesInst() { //Only for Strikers
        for (int waveIndex = startingWaveIndex; waveIndex < waves.Count; waveIndex++) {
            SpawnEnemiesInWaveInst(waveIndex);
        }
    }
    public void SpawnEnemiesInWaveInst(int waveIndex) { //Only for Strikers
        finishedSpawningEnemiesInWave = false;

        var currentWave = waves[waveIndex];
        for (int enemyCount = 1; enemyCount <= currentWave.GetNumEnemies(); enemyCount++) {
            var newEnemy = Instantiate(currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].GetPosition(),
                transform.rotation);
            newEnemy.GetComponent<EnemyMovement>().SetWaveConfig(currentWave);
        }

        finishedSpawningEnemiesInWave = true;
    }
    public bool GetFinishedSpawningEnemiesInWave() { return finishedSpawningEnemiesInWave; }
    public List<WaveConfig> GetWaves() { return waves; }
    public bool GetIsStrikerSpawner() { return isStrikerSpawner; }
}
