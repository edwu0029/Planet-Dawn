using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [Header("Heath")] //Health Variables
    [SerializeField] int maxHealth = 100;
    int health = 100;
    bool disableDealDamage = false;

    [Header("Death")] //Death Variables
    [SerializeField] float deathVFXDelay = 1f;
    [SerializeField] GameObject deathVFX;

    Level level;

    void Start() {
        health = maxHealth;
        level = FindObjectOfType<Level>();
    }
    public void DealDamage(int damage) {
        if (!disableDealDamage) {
            health = Mathf.Max(0, health - damage);
        }
        if (health <= 0) {
            Death();
        }
    }
    public void HealHealth(int amountHealed) {
        health = Mathf.Clamp(health + amountHealed, 0, maxHealth);
    }
    public void Death() {
        GameObject currentDeathVFX = Instantiate(deathVFX, transform.position, transform.rotation) as GameObject;
        Destroy(currentDeathVFX, deathVFXDelay);
        if (gameObject.GetComponent<Enemy>()) {
            level.AddScore(gameObject.GetComponent<Enemy>().GetPointValue());
            level.DecreaseNumOfEnemiesLeft();
        } else if (gameObject.GetComponent<EnemyBoss>()) {
            level.AddScore(gameObject.GetComponent<EnemyBoss>().GetPointValue());
            LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
            levelLoader.LoadBossWinScene();
        } else if (gameObject.GetComponent<Player>()) {
            LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
            FindObjectOfType<HealthBar>().SetHealth(health);
            levelLoader.LoadGameOver();
        }
        Destroy(gameObject);
    }
    public int GetHealth() { return health; }
}
