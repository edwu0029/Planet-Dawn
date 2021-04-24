using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {
    [Header("Points")]
    [SerializeField] int pointValue = 500;
    void Start() {
        
    }
    void Update() {
        
    }
    public int GetPointValue() { return pointValue; }
}
