using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D otherCollider) {
        Destroy(otherCollider.gameObject);
    }
}
