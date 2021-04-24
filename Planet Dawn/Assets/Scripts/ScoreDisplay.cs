using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
    Level level;
    Text scoreText;

    void Start() {
        level = FindObjectOfType<Level>();
        scoreText = GetComponent<Text>();
    }
    void Update() {
        UpdateScoreText();
    }
    private void UpdateScoreText() {
        scoreText.text = level.GetScore().ToString();
    }

}
