using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    [SerializeField] float scrollSpeed = 0.2f;

    Material backgroundMaterial;

    // Start is called before the first frame update
    void Start() {
        backgroundMaterial = GetComponent<Renderer>().material;
        backgroundMaterial.mainTextureOffset = new Vector2(0f, Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update() {
        var curOffset = backgroundMaterial.mainTextureOffset;
        backgroundMaterial.mainTextureOffset = new Vector2(0f, curOffset.y + scrollSpeed * Time.deltaTime);
    }
}
