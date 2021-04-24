using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {
    [Header("Score")] //Score/Points Variables
    [SerializeField] int score = 0;

    [SerializeField] Image boundaryReference;
    float minX, maxX, minY, maxY;

    [Header("Level")] //Level based Variables
    [SerializeField] int levelNumber;
    [SerializeField] bool isBossLevel = false;
    [SerializeField] bool looping = false;
    int numOfEnemiesLeft = 0;

    EnemySpawner[] enemySpawners;
    List<float>[] delayBetweenSpawners = new List<float>[5];

    private void Awake() {
        SetUpSingleton();
    }
    void Start() {
        SetBoundaries();
        enemySpawners = FindObjectsOfType<EnemySpawner>();
        Array.Sort(enemySpawners, SortEnemySpawners);
        CountEnemies();
        SetDelayBetweenSpawners();
        StartCoroutine(InitiateSpawners());
    }
    void Update() {
        
    }
    private void SetUpSingleton() {
        int numLevels = FindObjectsOfType<Level>().Length;
        if (numLevels > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void CountEnemies() {
        foreach(EnemySpawner enemySpawner in enemySpawners) {
            Debug.Log(enemySpawner.name);
            List<WaveConfig> waves = enemySpawner.GetWaves();
            foreach(WaveConfig wave in waves) {
                numOfEnemiesLeft += wave.GetNumEnemies();
            }
        }
        var enemyBosses = FindObjectsOfType<EnemyBoss>();
        if (enemyBosses.Length > 0) {
            numOfEnemiesLeft += enemyBosses.Length;
        }
    }
    private void SetDelayBetweenSpawners() {
        delayBetweenSpawners[1] = new List<float> { 2f, 3f, 2f, 3f};
        delayBetweenSpawners[2] = new List<float> { 3f, 1.5f, 4f, 4f, 4f, 3.5f, 1f};
        delayBetweenSpawners[3] = new List<float> { 1f, 2f, 4f, 4f, 1f, 4f, 5f, 5f};
    }
    private void SetBoundaries() {
        Vector3[] corners = new Vector3[4];
        boundaryReference.GetComponent<RectTransform>().GetWorldCorners(corners);
        //Index: 0 -> bot left, 1 -> top left, 2 -> top right, 3 -> bot right
        minX = corners[0].x;
        maxX = corners[2].x;

        minY = corners[3].y;
        maxY = corners[1].y;
    }
    public void AddScore(int value) {
        score += value;
    }
    public void DecreaseNumOfEnemiesLeft() {
        numOfEnemiesLeft--;
        if (!isBossLevel && numOfEnemiesLeft <= 0) {
            FindObjectOfType<LevelLoader>().LoadWinScene();
        }
    }
    private IEnumerator InitiateSpawners() {
        do {
            for (int enemySpawnerIndex = 0; enemySpawnerIndex < enemySpawners.Length; enemySpawnerIndex++) {
                if (enemySpawners[enemySpawnerIndex].GetIsStrikerSpawner()) {
                    enemySpawners[enemySpawnerIndex].SpawnAllWavesInst();
                } else {
                    StartCoroutine(enemySpawners[enemySpawnerIndex].SpawnAllWaves());
                }
                while (!enemySpawners[enemySpawnerIndex].GetFinishedSpawningEnemiesInWave()) {
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                yield return new WaitForSeconds(delayBetweenSpawners[levelNumber][enemySpawnerIndex]);
            }
        } while (looping);
    }
    public int SortEnemySpawners(EnemySpawner e1, EnemySpawner e2) {
        return e1.name.CompareTo(e2.name);
    }
    public int GetNumOfEnemiesLeft() { return numOfEnemiesLeft; }
    public int GetLevelNumber() { return levelNumber; }
    public float GetMinX() { return minX; }
    public float GetMaxX() { return maxX; }
    public float GetMinY() { return minY; }
    public float GetMaxY() { return maxY; }
    public int GetScore() { return score; }
}
