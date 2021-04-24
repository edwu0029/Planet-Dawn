using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveConfig")]
public class WaveConfig : ScriptableObject {
    [SerializeField] GameObject pathPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] int numEnemies = 3;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float timeBetweenSpawns = 1f;

    [SerializeField] float shootDelayMin = 0.5f;
    [SerializeField] float shootDelayMax = 2f;

    public List<Waypoint> GetWaypoints() {
        List<Waypoint> waveWaypoints = new List<Waypoint>();
        foreach (Transform child in pathPrefab.transform) {
            child.gameObject.GetComponent<Waypoint>().SetPos();
            waveWaypoints.Add(child.gameObject.GetComponent<Waypoint>());
        }
        return waveWaypoints;
    }
    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public int GetNumEnemies() { return numEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetShootDelayMin() { return shootDelayMin; }
    public float GetShootDelayMax() { return shootDelayMax; }
}
