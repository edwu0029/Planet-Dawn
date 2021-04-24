using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Movement")] //Movement Variables
    float paddingX = 0.5f;
    float paddingY = 0.4f;
    [SerializeField] float moveSpeed = 3f;

    [Header("Shooting")] //Shooting Variables
    [SerializeField] float shootDelay = 0.3f;
    float shootOffsetY = 0.4f;
    [SerializeField] GameObject projectile;

    [Header("Death")] //Death Variables
    [SerializeField] float deathVFXDelay = 1f;
    [SerializeField] GameObject deathVFX;

    //Cached Components
    Health health;

    //Cached Game Objects
    Level level;
    HealthBar healthBar;

    //Misc variables
    Coroutine shootContinuously;



    // Start is called before the first frame update
    void Start() {
        health = GetComponent<Health>();
        level = FindObjectOfType<Level>();
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(health.GetHealth());
    }

    // Update is called once per frame
    void Update() {
        healthBar.SetHealth(health.GetHealth());
        Move();
        if (Input.GetKeyDown(KeyCode.Space)) {
            shootContinuously = StartCoroutine(ShootContinuously());
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            StopCoroutine(shootContinuously);
        }
    }
    private void Move() {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newX = Mathf.Clamp(transform.position.x + deltaX, level.GetMinX() + paddingX, level.GetMaxX() - paddingX);
        float newY = Mathf.Clamp(transform.position.y + deltaY, level.GetMinY() + paddingY, level.GetMaxY() - paddingY);

        transform.position = new Vector2(newX, newY);
    }
    private IEnumerator ShootContinuously() {
        while (true) {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }
    private void Shoot() {
        Vector2 shootLocation = new Vector2(transform.position.x, transform.position.y + shootOffsetY);
        var currentProjectile = Instantiate(projectile, shootLocation, transform.rotation) as GameObject;
        currentProjectile.GetComponent<Projectile>().SetVelocity();
    }
}
