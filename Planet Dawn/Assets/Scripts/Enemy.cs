using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Points")] //Point/Score Variables
    int pointValue = 10;

    [Header("Shooting")] //Shooting Variables
    [SerializeField] bool disableShooting = false;
    [SerializeField] float shootDelayMin = 0.5f;
    [SerializeField] float shootDelayMax = 2f;
    float shootDelay = 0f;
    [SerializeField] GameObject projectile;

    [Header("Collision")] //Collision with Player variables
    int collisionDamage = 50;

    //Cached Components
    Health health;

    //Cached Game Objects
    Level level;

    //Misc variables

    void Start() {
        health = GetComponent<Health>();
        level = FindObjectOfType<Level>();
    }
    void Update() {
        if (!disableShooting) {
            ShootContinuously();
        }
    }
    private void ShootContinuously() {
        shootDelay -= Time.deltaTime;
        if (shootDelay <= Mathf.Epsilon) {
            Shoot();
            shootDelay = Random.Range(shootDelayMin, shootDelayMax);
        }
    }
    private void Shoot() {
        var currentProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        currentProjectile.GetComponent<Projectile>().SetVelocity();
    }
    private void OnCollisionEnter2D(Collision2D otherCollider) {
        if (otherCollider.gameObject.GetComponent<Player>()) {
            otherCollider.gameObject.GetComponent<Health>().DealDamage(collisionDamage);
            GetComponent<Health>().Death();
        }
    }
    public void SetShootDelayMin(float newShootDelayMin) {
        shootDelayMin = newShootDelayMin;
    }
    public void SetShootDelayMax(float newShootDelayMax) {
        shootDelayMax = newShootDelayMax;
    }
    public int GetPointValue() { return pointValue; }
}
