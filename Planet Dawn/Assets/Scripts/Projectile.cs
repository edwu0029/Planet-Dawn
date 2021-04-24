using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] float speed = 10f;
    [SerializeField] float directionY = 1;
    [SerializeField] int damage = 50;
    public void SetVelocity() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, speed * directionY);
    }
    public int GetDamage() {
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D otherCollider) {
        Health healthComponent = otherCollider.gameObject.GetComponent<Health>(); //Player and Enemy
        if (healthComponent) {
            healthComponent.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
