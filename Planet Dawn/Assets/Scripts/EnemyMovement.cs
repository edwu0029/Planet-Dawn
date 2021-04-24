using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [Header("WaveConfig")] //WaveConfig Variables
    WaveConfig waveConfig;
    List<Waypoint> waypoints;
    int wavepointIndex = 0;

    [Header("Enemy Movement")] //Movement Variables
    float moveSpeed = 0f;

    [Header("Delays")] //Delay Variables
    float delayTimeLeft = 0f;
    bool delayStarted = false;

    Level level;

    void Start() {
        level = FindObjectOfType<Level>();
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
        GetComponent<Enemy>().SetShootDelayMin(waveConfig.GetShootDelayMin());
        GetComponent<Enemy>().SetShootDelayMax(waveConfig.GetShootDelayMax());
        transform.position = waypoints[wavepointIndex].GetPosition();
    }
    void Update() {
        Move();
    }
    public void SetWaveConfig(WaveConfig wave) {
        waveConfig = wave;
    }
    private void Move() {
        if (wavepointIndex <= waypoints.Count - 1) {
            var targetPosition = waypoints[wavepointIndex].GetPosition();
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition) {
                if (waypoints[wavepointIndex].GetDelay() > Mathf.Epsilon && !delayStarted) {
                    delayTimeLeft = waypoints[wavepointIndex].GetDelay();
                    delayStarted = true;
                    return;
                } else if (delayTimeLeft > Mathf.Epsilon && delayStarted) {
                    delayTimeLeft -= Time.deltaTime;
                    return;
                }else if (delayTimeLeft <= Mathf.Epsilon && delayStarted) {
                    delayStarted = false;
                }
                wavepointIndex++;
            }
        } else {
            level.DecreaseNumOfEnemiesLeft();
            Destroy(gameObject);
        }
    }
}
