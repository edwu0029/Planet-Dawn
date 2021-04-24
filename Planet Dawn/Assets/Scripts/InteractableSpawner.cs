using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpawner : MonoBehaviour {
    [SerializeField] GameObject healInteractable;
    [SerializeField] float spawnDelayMin = 5f;
    [SerializeField] float spawnDelayMax = 5f;

    float spawnPosY = 6f;
    float spawnPaddingX = 1f;
    float spawnDelay = 3f;

    Level level;
    Player player;

    void Start() {
        level = FindObjectOfType<Level>();
        player = FindObjectOfType<Player>();
    }
    void Update() {
        if (player && player.GetComponent<Health>().GetHealth() <= 50) {
            if (spawnDelay <= Mathf.Epsilon) {
                SpawnHealInteractable();
                spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
            } else {
                spawnDelay -= Time.deltaTime;
            }
        }
    }
    private void SpawnHealInteractable() {
        Vector2 pos = new Vector2(Random.Range(level.GetMinX() + spawnPaddingX, level.GetMaxX() - spawnPaddingX), spawnPosY);
        Instantiate(healInteractable, pos, transform.rotation);
    }
    
}
