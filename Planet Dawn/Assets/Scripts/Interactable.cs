using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField] string interactableType;
    [SerializeField] float speed = -3f;

    [Header("Heal")] //Heal Interactable Variable
    int healAmount = 40;
    HealthBar healthBar;

    private void Start() {
        SetVelocity();
    }
    public void SetVelocity() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, speed);
    }
    public void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.gameObject.GetComponent<Player>()) {
            ActivateInteractable();
        }
        Destroy(gameObject);
    }
    public void ActivateInteractable() {
        if (interactableType == "Heal") {
            Health playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
            healthBar = FindObjectOfType<HealthBar>();

            playerHealth.HealHealth(healAmount);
            healthBar.SetHealth(playerHealth.GetHealth());
        }
    }
}
