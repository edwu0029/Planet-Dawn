using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    [SerializeField] float delay = 0f;
    Vector3 pos;
    public void SetPos() {
        pos = transform.position;
    }
    public float GetDelay() { return delay; }
    public Vector3 GetPosition() { return pos; }
}
