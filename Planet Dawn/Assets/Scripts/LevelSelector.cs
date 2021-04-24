using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {
    [SerializeField] int selectedLevelNumber = 0;
    [SerializeField] Text selectedLevelText;

    LevelLoader levelLoader;
    LevelDescription levelDescription;

    void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
        levelDescription = FindObjectOfType<LevelDescription>();
    }
    void Update() {
        
    }

    public void SetSelectedLevel(int newLevelNumber) {
        selectedLevelNumber = newLevelNumber;
        selectedLevelText.text = "Selected Level: " + selectedLevelNumber.ToString();
        levelDescription.UpdateLevelDescripton(selectedLevelNumber);
    }
    public void LoadSelectedLevel() {
        levelLoader.LoadLevel(selectedLevelNumber);
    }
    public int GetSelectedLevelNumber() { return selectedLevelNumber; }
}
